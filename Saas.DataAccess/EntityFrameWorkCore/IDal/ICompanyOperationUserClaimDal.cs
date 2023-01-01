using Saas.Entities.Generic;
using Saas.Entities.Models.UserClaims;

namespace Saas.DataAccess.EntityFrameWorkCore.IDal
{
    public interface ICompanyOperationUserClaimDal :IEntityRepository<CompanyOperationUserClaim>,IEntityRepositoryAsync<CompanyOperationUserClaim>
    {
    }
}
