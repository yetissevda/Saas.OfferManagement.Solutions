using System;
using System.Collections.Generic;
using Saas.Core.Utilities.Results;
using Saas.Entities.Models.UserClaims;

namespace Saas.Business.Abstract
{
    public interface IOperationClaimService
    {
        IDataResult<List<CompanyOperationClaim>> GetList();
        IDataResult<CompanyOperationClaim> GetById(Guid rolesId);
        IResult Add(CompanyOperationClaim roles);
        IResult Delete(CompanyOperationClaim roles);
        IResult Update(CompanyOperationClaim roles);
    }
}
