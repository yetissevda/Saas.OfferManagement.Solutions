using Saas.DataAccess.EntityFrameWorkCore.DbContexts;
using Saas.DataAccess.EntityFrameWorkCore.IDal;
using Saas.Entities.Generic;
using Saas.Entities.Models.User;

namespace Saas.DataAccess.EntityFrameWorkCore.EfDal
{
    public class EfCompanyUserBranchesDal :EfEntityRepositoryBase<CompanyUserBranches,GordionDbContext>, ICompanyUserBranchesDal
    {
    }
}
