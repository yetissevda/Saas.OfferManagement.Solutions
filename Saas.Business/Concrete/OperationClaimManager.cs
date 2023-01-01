using Saas.Business.Abstract;
using Saas.Business.Constants;
using Saas.Core.Aspect.Autofac.Caching;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.Aspect.Autofac.Performance;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Saas.Core.Utilities.Results;
using Saas.DataAccess.EntityFrameWorkCore.IDal;
using Saas.Entities.Models.UserClaims;
using System;
using System.Collections.Generic;

namespace Saas.Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly ICompanyOperationClaimDal _rolesDal;

        public OperationClaimManager(ICompanyOperationClaimDal rolesDal)
        {
            _rolesDal = rolesDal;
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Add(CompanyOperationClaim roles)
        {
            _rolesDal.Add(roles);
            return new DataResult<CompanyOperationClaim>(Messages.rolesAdded);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(CompanyOperationClaim roles)
        {
            _rolesDal.Delete(roles);
            return new DataResult<CompanyOperationClaim>(Messages.rolesDeleted);

        }
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<CompanyOperationClaim> GetById(Guid rolesId)
        {
            return new DataResult<CompanyOperationClaim>(_rolesDal.Get(p => p.ID == rolesId), true);
        }
        [CacheAspect(duration: 10)]  //10 dakika boyunca cache te sonra db den tekrar cache e seklinde bir dongu
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(interval: 5)]
        public IDataResult<List<CompanyOperationClaim>> GetList()
        {
            return new SuccessDataResult<List<CompanyOperationClaim>>(_rolesDal.GetList());
        }
        //[CacheAspect(duration: 10)]  //10 dakika boyunca cache te sonra db den tekrar cache e seklinde bir dongu
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(interval: 5)]
        public IResult Update(CompanyOperationClaim roles)
        {
            _rolesDal.Update(roles, roles.ID);
            return new DataResult<CompanyOperationClaim>(message: Messages.rolesUpdated);
        }
    }
}
