using Microsoft.Data.SqlClient;
using Saas.Business.Abstract.Branch;
using Saas.Core.Aspect.Autofac.Caching;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Saas.Core.Utilities.Results;
using Saas.DataAccess.EntityFrameWorkCore.IDal;
using Saas.Entities.Generic;
using Saas.Entities.Models.Branch;
using Saas.Entities.Models.User;
using Saas.WebCoreApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using Saas.Entities.Models;

namespace Saas.Business.Concrete.Branch
{
    public class CompanyBranchesManager : ICompanyBranchesService
    {
        private readonly ICompanyBranchDal _branchDal;
        private readonly ICompanyDal _companyDal;

        public CompanyBranchesManager(ICompanyBranchDal branchDal, ICompanyDal companyDal)
        {
            _branchDal = branchDal;
            _companyDal = companyDal;
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Add(CompanyBranch companyBranch)
        {
            var company = _companyDal.Get(x => x.ID == companyBranch.CompanyId)??new Company();
            companyBranch.Company = company;
            _branchDal.Add(companyBranch);
            return new SuccessResult("");
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<CompanyBranch> CompanyBranchById(Guid branchId)
        {
            var lst = _branchDal.GetList().Where(x => x.ID == branchId).FirstOrDefault();
            return new SuccessDataResult<CompanyBranch>(lst, "");
        }
        [CacheAspect(duration: 10)]  //10 dakika boyunca cache te sonra db den tekrar cache e seklinde bir dongu
        [LogAspect(typeof(DatabaseLogger))]

        public IDataResult<List<CompanyBranch>> CompanyBranchesList()
        {
            var lst = _branchDal.GetList();
            return new SuccessDataResult<List<CompanyBranch>>(lst, "");
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(CompanyBranch companyBranch)
        {
            _branchDal.Delete(companyBranch);
            return new SuccessResult("");
        }

        [LogAspect(typeof(DatabaseLogger))]
        public IResult Update(CompanyBranch companyBranch)
        {
            _branchDal.Update(companyBranch, companyBranch.ID);
            return new SuccessResult("");
        }

        //sample data
        public IDataResult<IDto> SqlHelper(string query)
        {
            List<CompanyBranch> usersInDb = _branchDal.FromSqlQuery(
                    query,
                    x => new CompanyBranch
                    {
                        Description = (string)x[0],
                        DescriptionThree = (string)x[1]
                    },
                    new SqlParameter("@paramName", "")
                )
                .ToList();
            List<CompanyBranch> userssInDb = _branchDal.FromSqlQuery<CompanyBranch>
                (
                    query,
                    new SqlParameter("@paramName", "user.Name")
                )
                .ToList();
            return new SuccessDataResult<IDto>("");
        }
    }
}
