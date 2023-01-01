using Saas.Business.Abstract.User;
using Saas.Business.Constants;
using Saas.Core.Aspect.Autofac.Caching;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Saas.Core.Utilities.Results;
using Saas.DataAccess.EntityFrameWorkCore.IDal;
using Saas.Entities.Models.UserClaims;
using System;
using System.Collections.Generic;

namespace Saas.Business.Concrete.User
{
    public class CompanyOperationUserClaimManager : ICompanyOperationUserClaimService
    {
        private readonly ICompanyOperationUserClaimDal _userOperationClaim;

        public CompanyOperationUserClaimManager(ICompanyOperationUserClaimDal userOperationClaim)
        {
            _userOperationClaim = userOperationClaim;
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Add(CompanyOperationUserClaim roles)
        {
            _userOperationClaim.Add(roles);
            return new DataResult<CompanyOperationUserClaim>(Messages.rolesAdded);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(CompanyOperationUserClaim roles)
        {
            _userOperationClaim.Delete(roles);
            return new DataResult<CompanyOperationUserClaim>(Messages.rolesDeleted);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<CompanyOperationUserClaim>> GetByRoleId(Guid operationClaimId)
        {
            return new DataResult<List<CompanyOperationUserClaim>>(_userOperationClaim.GetList(p => p.CompanyOperationClaimId == operationClaimId), true);
        }
        [CacheAspect(duration: 10)]  //10 dakika boyunca cache te sonra db den tekrar cache e seklinde bir dongu
        [LogAspect(typeof(DatabaseLogger))]

        public IDataResult<List<CompanyOperationUserClaim>> GetList()
        {
            return new DataResult<List<CompanyOperationUserClaim>>(_userOperationClaim.GetList(), true);
        }
        [CacheAspect(duration: 10)]  //10 dakika boyunca cache te sonra db den tekrar cache e seklinde bir dongu
        [LogAspect(typeof(DatabaseLogger))]

        public IResult Update(CompanyOperationUserClaim roles)
        {
            _userOperationClaim.Update(roles, roles.ID);
            return new DataResult<CompanyOperationUserClaim>(Messages.rolesUpdated);
        }
    }
}
