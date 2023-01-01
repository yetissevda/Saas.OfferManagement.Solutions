using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Identity;
using Saas.Business.Abstract.User;
using Saas.Business.Constants;
using Saas.Business.ValidationRules.BusinessRules;
using Saas.Business.ValidationRules.FluentValidation;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.Aspect.Autofac.Transaction;
using Saas.Core.Aspect.Autofac.Validation;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Saas.Core.Security.Security.Hashıng;
using Saas.Core.Security.Security.Jwt;
using Saas.Core.Utilities.Business;
using Saas.Core.Utilities.Results;
using Saas.DataAccess.EntityFrameWorkCore.IDal;
using Saas.Entities.Dto;
using Saas.Entities.Generic;
using Saas.Entities.Models;
using Saas.Entities.Models.Branch;
using Saas.Entities.Models.User;
using ServiceStack;

namespace Saas.Business.Concrete.User
{
    public class AuthManager : IAuthService
    {
        //private readonly ICompanyUserService _userService;
        private readonly ICompanyUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICompanyBranchDal _branchDal;
        private readonly ICompanyDal _companyDal;
        private readonly ICompanyUserBranchesDal _userBranchesDal;
        private ICompanyUserBranchesService _companyUserBranchesService;

        public AuthManager(ICompanyUserDal usersService, ITokenHelper tokenHelper, ICompanyBranchDal branchDal, ICompanyDal companyDal, ICompanyUserBranchesDal userBranchesDal, ICompanyUserBranchesService companyUserBranchesService)
        {
            //_userService = usersService;
            _userDal = usersService;
            _tokenHelper = tokenHelper;
            _branchDal = branchDal;
            _companyDal = companyDal;
            _userBranchesDal = userBranchesDal;
            _companyUserBranchesService = companyUserBranchesService;
        }

        [ValidationAspect(typeof(UserValidation), Priority = 1)]
        [LogAspect(typeof(DatabaseLogger))]
        [TransactionScopeAspect]
        public IDataResult<CompanyUser> Register(CompanyUserDto userForRegisterDto)
        {
            //byte[] passwordHash, passwordSalt;
            IResult result = BusinessRules.Run(EmailValidation.Run(userForRegisterDto.Email));
            if (result != null)
                return new ErrorDataResult<CompanyUser>(result.Message);

            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var usr = new CompanyUser
            {
                CompanyId = userForRegisterDto.CompanyId,
                //   UserBranches = userForRegisterDto.UserBranchesList,
                Email = userForRegisterDto.Email,
                FullName = userForRegisterDto.FullName,
                PassWordHash = passwordHash,
                PassWordSalt = passwordSalt,
                IsStudent = userForRegisterDto.IsStudent,
                SysAdmin = userForRegisterDto.SysAdmin,
                BranchAdmin = userForRegisterDto.BranchAdmin,
                Deleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "Admin"
            };
            //  _userService.Add(usr);
            _userDal.Add(usr);
            foreach (var br in userForRegisterDto.UserBranchesList)
            {
                _userBranchesDal.Add(new CompanyUserBranches()
                {
                    CompanyUserId = usr.ID,
                    BranchId = br,
                    IsAdmin = userForRegisterDto.BranchAdmin,
                    Deleted = false,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "Admin"
                });
            }

            return new DataResult<CompanyUser>(usr, true, Messages.UsersAdded);
        }


        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<CompanyUser> Login(UserForLoginDto userForLoginDto)
        {
            IResult result = BusinessRules.Run(EmailValidation.Run(userForLoginDto.Email));
            if (result != null)
                return new DataResult<CompanyUser>(result.Message);

            //var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            var userToCheck = _userDal.Get(x => x.Email == userForLoginDto.Email);
            if (userToCheck == null)
                return new ErrorDataResult<CompanyUser>(Messages.UserNotFound);

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PassWordHash, userToCheck.PassWordSalt))
            {
                return new ErrorDataResult<CompanyUser>(Messages.PasswordError);
            }

            return new DataResult<CompanyUser>(userToCheck, true, Messages.SuccessfullLogin);


        }

        [ValidationAspect(typeof(AuthValidator), Priority = 1)]
        [LogAspect(typeof(DatabaseLogger))]
        [TransactionScopeAspect]
        public IResult RegisterForCompany(CompanyFirstRegisterDto dt)
        {
            var result = BusinessRules.Run(CheckCompanyTaxNumberExist(dt.TaxNumber));

            if (result != null)
                return result;

            var resultsForMail = BusinessRules.Run(EmailValidation.Run(dt.Email));
            if (resultsForMail != null)

                return new ErrorDataResult<CompanyUser>(resultsForMail.Message);
            var mailcheck = UserExist(dt.Email);
            if (!mailcheck.Success)
                return mailcheck;

            Company company = new()
            {
                TaxNumber = dt.TaxNumber,
                Adress = dt.Adress,
                FullName = dt.CompanyName,
                Deleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "Admin"
            };

            _companyDal.Add(company);

            CompanyBranch branch = new CompanyBranch
            {
                CompanyId = company.ID,
                FullName = "Merkez",
                Deleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "Admin"

            };
            _branchDal.Add(branch);

            dt.CompanyId = company.ID;

            if (string.IsNullOrWhiteSpace(dt.Password) || string.IsNullOrEmpty(dt.Password))
                dt.Password = GenerateRandomPassword(new PasswordOptions()
                {
                    RequireDigit = true,
                    RequireUppercase = true,
                    RequiredLength = 5
                });
            var rt = UserExist(dt.Email);
            if (!rt.Success)
                return new ErrorDataResult<CompanyFirstRegisterDto>(message: rt.Message);

            var usr = Register(dt);

            if (usr.Success)
            {
                _userBranchesDal.Add(new
                    CompanyUserBranches()
                {
                    Branch = branch,
                    CompanyUser = usr.Data,
                    CompanyUserId = usr.Data.ID,
                    BranchId = branch.ID,
                    IsAdmin = usr.Data.BranchAdmin,
                    Deleted = false,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "Admin",

                });
            }

            usr.Data.Company = company;
            var branchlst = _companyUserBranchesService.GetCompanyUserBranchesList().Result;
            usr.Data.UserBranches = branchlst.Success ? branchlst.Data.Where(x => x.CompanyUserId == usr.Data.ID).ToList() : new EditableList<CompanyUserBranches>();
            Login(new UserForLoginDto()
            {
                Email = dt.Email,
                Password = dt.Password
            });

            return new SuccessDataResult<CompanyUser>(usr.Data, Messages.CompanyAdded);
        }
        [ValidationAspect(typeof(UserValidation), Priority = 1)]
        [LogAspect(typeof(DatabaseLogger))]
        [TransactionScopeAspect]
        private IDataResult<CompanyUser> Register(CompanyFirstRegisterDto userForRegisterDto)
        {
            //byte[] passwordHash, passwordSalt;
            IResult result = BusinessRules.Run(EmailValidation.Run(userForRegisterDto.Email));
            if (result != null)
                return new ErrorDataResult<CompanyUser>(result.Message);

            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var usr = new CompanyUser
            {
                CompanyId = userForRegisterDto.CompanyId,
                //   UserBranches = userForRegisterDto.UserBranchesList,
                Email = userForRegisterDto.Email,
                FullName = userForRegisterDto.FullName,
                PassWordHash = passwordHash,
                PassWordSalt = passwordSalt,
                IsStudent = userForRegisterDto.IsStudent,
                SysAdmin = userForRegisterDto.SysAdmin,
                BranchAdmin = userForRegisterDto.BranchAdmin,
                Deleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "Admin"
            };
            //  _userService.Add(usr);
            _userDal.Add(usr);
            foreach (var br in userForRegisterDto.UserBranchesList)
            {
                _userBranchesDal.Add(new CompanyUserBranches()
                {
                    CompanyUserId = usr.ID,
                    BranchId = br,
                    IsAdmin = userForRegisterDto.BranchAdmin,
                    Deleted = false,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "Admin"
                });
            }
            var branchlst = _companyUserBranchesService.GetCompanyUserBranchesList().Result;
            usr.UserBranches = branchlst.Success ? branchlst.Data.Where(x => x.CompanyUserId == usr.ID).ToList() : new EditableList<CompanyUserBranches>();
            return new SuccessDataResult<CompanyUser>(usr,  Messages.UsersAdded);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult UserExist(string email)
        {
            //if (_userService.GetByMail(email) != null)
            //{
            if (_userDal.Get(x => x.Email == email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }

            return new SuccessResult();
        }

        //  [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<AccessToken> CreateAccessToken(CompanyUser user)
        {
            // var claims = _userService.GetClaims(user);
            var claims = _userDal.GetClaims(user);
            var accesstoken = _tokenHelper.CreateToken(user, claims);
            return new DataResult<AccessToken>(accesstoken, true, Messages.AccessTokenCreated);
        }




        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            opts ??= new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
        private IResult CheckCompanyTaxNumberExist(string companyTaxNumber)
        {
            // var data = _companyDal.GetCompanyList();
            if (_companyDal.GetList(p => p.TaxNumber.Equals(companyTaxNumber)).Count != 0)
            {
                return new ErrorResult(message: Messages.CompanyTaxNumberExistError);
            }
            return new SuccessResult();
        }

        public IDataResult<IDto> SqlHelper(string query)
        {
            throw new NotImplementedException();
        }

    }
}
