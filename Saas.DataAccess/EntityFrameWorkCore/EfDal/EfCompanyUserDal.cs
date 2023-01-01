using Microsoft.EntityFrameworkCore;
using Saas.DataAccess.EntityFrameWorkCore.DbContexts;
using Saas.DataAccess.EntityFrameWorkCore.IDal;
using Saas.Entities.Generic;
using Saas.Entities.Models.User;
using Saas.Entities.Models.UserClaims;

namespace Saas.DataAccess.EntityFrameWorkCore.EfDal
{
    public class EfCompanyUserDal :EfEntityRepositoryBase<CompanyUser,GordionDbContext>, ICompanyUserDal
    {
        public List<CompanyOperationClaim> GetClaims(CompanyUser user)
        {
            using var context = new GordionDbContext();
            var result = from operationClaim in context.CompanyOperationClaim
                         join companyOperationUserClaim in context.CompanyOperationUserClaim
                         on operationClaim.ID equals companyOperationUserClaim.CompanyOperationClaimId
                         where companyOperationUserClaim.CompanyUserId == user.ID
                         select new CompanyOperationClaim { ID = operationClaim.ID,Name = operationClaim.Name };
            return result.ToList();
            // return new List<CompanyOperationClaim>();
        }

        public async Task<List<CompanyOperationClaim>> GetClaimsAsync(CompanyUser user)
        {
            using var context = new GordionDbContext();
            var result = from operationClaim in context.CompanyOperationClaim
                         join companyOperationUserClaim in context.CompanyOperationUserClaim
                             on operationClaim.ID equals companyOperationUserClaim.CompanyOperationClaimId
                         where companyOperationUserClaim.CompanyUserId == user.ID
                         select new CompanyOperationClaim { ID = operationClaim.ID,Name = operationClaim.Name };
            return await result.ToListAsync();
        }
    }
}
