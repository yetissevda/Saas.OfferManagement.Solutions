using Saas.Entities.Generic;
using Saas.Entities.Models;

namespace Saas.DataAccess.EntityFrameWorkCore.IDal;

public interface ICompanyDal :IEntityRepository<Company>,IEntityRepositoryAsync<Company>
{

}