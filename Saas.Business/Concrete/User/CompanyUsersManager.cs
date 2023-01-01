using Saas.Business.Abstract.User;
using Saas.Business.Constants;
using Saas.Business.ValidationRules.BusinessRules;
using Saas.Business.ValidationRules.FluentValidation;
using Saas.Core.Aspect.Autofac.Caching;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.Aspect.Autofac.Transaction;
using Saas.Core.Aspect.Autofac.Validation;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Saas.Core.Security.Security.Hashıng;
using Saas.Core.Utilities.Business;
using Saas.Core.Utilities.Results;
using Saas.DataAccess.EntityFrameWorkCore.IDal;
using Saas.Entities.Dto;
using Saas.Entities.Generic;
using Saas.Entities.Models.User;
using Saas.Entities.Models.UserClaims;
using System;
using System.Collections.Generic;
using Saas.Core.Security.Security.Jwt;

namespace Saas.Business.Concrete.User
{
    public class CompanyUsersManager : ICompanyUserService
    {

        private readonly ICompanyUserDal _userDal;
        private readonly ICompanyUserBranchesDal _userBranchesDal;

        public CompanyUsersManager(ICompanyUserDal userDal, ICompanyUserBranchesDal userBranchesDal)
        {
            _userDal = userDal;
            _userBranchesDal = userBranchesDal;
        }

        [ValidationAspect(typeof(UserValidation), Priority = 1)]
        [LogAspect(typeof(DatabaseLogger))]
        [TransactionScopeAspect]
        public IDataResult<CompanyUser> Register(CompanyUserDto userDto)
        {
            //byte[] passwordHash, passwordSalt;
            IResult result = BusinessRules.Run(EmailValidation.Run(userDto.Email));
            if (result != null)
                return new ErrorDataResult<CompanyUser>(result.Message);

            HashingHelper.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var usr = new CompanyUser
            {
                CompanyId = userDto.CompanyId,
                //   UserBranches = userForRegisterDto.UserBranchesList,
                Email = userDto.Email,
                FullName = userDto.FullName,
                PassWordHash = passwordHash,
                PassWordSalt = passwordSalt,
                IsStudent = userDto.IsStudent,
                SysAdmin = userDto.SysAdmin,
                BranchAdmin = userDto.BranchAdmin,
                Deleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "Admin"
            };
            _userDal.Add(usr);
            foreach (var br in userDto.UserBranchesList)
            {
                _userBranchesDal.Add(new CompanyUserBranches()
                {
                    CompanyUserId = usr.ID,
                    BranchId = br,
                    IsAdmin = userDto.BranchAdmin,
                    Deleted = false,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "Admin"
                });
            }

            return new DataResult<CompanyUser>(usr, true, Messages.UsersAdded);
        }

        [ValidationAspect(typeof(UserValidation), Priority = 1)]
        [LogAspect(typeof(DatabaseLogger))]
        [TransactionScopeAspect]
        public IDataResult<CompanyUser> UpdateUser(CompanyUserUpdateDto userDto)
        {
            if (userDto.UserId == Guid.Empty) return new ErrorDataResult<CompanyUser>("Id Not Found");
            //byte[] passwordHash, passwordSalt;
            IResult result = BusinessRules.Run(EmailValidation.Run(userDto.Email));
            if (result != null)
                return new ErrorDataResult<CompanyUser>(result.Message);

            HashingHelper.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var usr = new CompanyUser
            {
                CompanyId = userDto.CompanyId,
                //   UserBranches = userForRegisterDto.UserBranchesList,
                Email = userDto.Email,
                FullName = userDto.FullName,
                PassWordHash = passwordHash,
                PassWordSalt = passwordSalt,
                IsStudent = userDto.IsStudent,
                SysAdmin = userDto.SysAdmin,
                BranchAdmin = userDto.BranchAdmin,
                Deleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "Admin"
            };
            //  _userService.Add(usr);
            _userDal.Update(usr, usr.ID);
            foreach (var br in userDto.UserBranchesList)
                _userBranchesDal.Update(new CompanyUserBranches()
                {
                    CompanyUserId = usr.ID,
                    IsAdmin = userDto.BranchAdmin,
                    Deleted = false,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "Admin"
                }, br);
            return new DataResult<CompanyUser>(usr, true, Messages.UsersAdded);
        }


       

        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(CompanyUser user)
        {
            _userDal.Delete(user);
            return new DataResult<CompanyUser>(Messages.UsersDeleted);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<CompanyUser> GetUserById(Guid userId)
        {
            return new DataResult<CompanyUser>(_userDal.Get(p => p.ID == userId), true);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public CompanyUser GetByMail(string mail)
        {
            return _userDal.Get(p => p.Email == mail);
        }
        [CacheAspect(duration: 10)]  //10 dakika boyunca cache te sonra db den tekrar cache e seklinde bir dongu
        [LogAspect(typeof(DatabaseLogger))]

        public List<CompanyOperationClaim> GetClaims(CompanyUser user)
        {
            return _userDal.GetClaims(user);
        }
        [CacheAspect(duration: 10)]  //10 dakika boyunca cache te sonra db den tekrar cache e seklinde bir dongu
        [LogAspect(typeof(DatabaseLogger))]

        public IDataResult<List<CompanyUser>> GetUserList()
        {
            return new DataResult<List<CompanyUser>>(_userDal.GetList(), true);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Update(CompanyUser user)
        {
            _userDal.Update(user, user.ID);
            return new DataResult<CompanyUser>(Messages.UsersUpdated);
        }

        public IDataResult<IDto> SqlHelper(string query)
        {
            throw new NotImplementedException();
        }
    }
}
