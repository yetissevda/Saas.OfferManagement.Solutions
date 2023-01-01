using Saas.Entities.Generic;
using Saas.Entities.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.DataAccess.EntityFrameWorkCore.IDal.Product
{
    public interface ICompanyProductUnitDal: IEntityRepository<CompanyProductUnits>, IEntityRepositoryAsync<CompanyProductUnits>
    {
    }
}
