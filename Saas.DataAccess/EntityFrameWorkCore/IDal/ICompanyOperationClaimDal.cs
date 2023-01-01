using Saas.Entities.Generic;
using Saas.Entities.Models.UserClaims;

namespace Saas.DataAccess.EntityFrameWorkCore.IDal
{
    public interface ICompanyOperationClaimDal :IEntityRepository<CompanyOperationClaim>,IEntityRepositoryAsync<CompanyOperationClaim>
    {
    }
}
