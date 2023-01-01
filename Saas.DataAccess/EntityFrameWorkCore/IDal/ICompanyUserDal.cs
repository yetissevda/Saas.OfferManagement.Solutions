using Saas.Entities.Generic;
using Saas.Entities.Models.User;
using Saas.Entities.Models.UserClaims;

namespace Saas.DataAccess.EntityFrameWorkCore.IDal
{
    public interface ICompanyUserDal :IEntityRepository<CompanyUser>, IEntityRepositoryAsync<CompanyUser>
    {
        List<CompanyOperationClaim> GetClaims(CompanyUser user);
        Task<List<CompanyOperationClaim>> GetClaimsAsync(CompanyUser user);
    }
}
