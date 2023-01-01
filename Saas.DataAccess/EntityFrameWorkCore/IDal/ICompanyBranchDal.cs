using Saas.Entities.Generic;
using Saas.Entities.Models.Branch;

namespace Saas.DataAccess.EntityFrameWorkCore.IDal
{
    public interface ICompanyBranchDal :IEntityRepository<CompanyBranch>, IEntityRepositoryAsync<CompanyBranch>
    {

    }
}
