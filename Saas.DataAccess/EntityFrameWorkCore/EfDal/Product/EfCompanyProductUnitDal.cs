using Saas.DataAccess.EntityFrameWorkCore.DbContexts;
using Saas.DataAccess.EntityFrameWorkCore.IDal.Product;
using Saas.Entities.Generic;
using Saas.Entities.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.DataAccess.EntityFrameWorkCore.EfDal.Product
{
    public class EfCompanyProductUnitDal : EfEntityRepositoryBase<CompanyProductUnits, GordionDbContext>, ICompanyProductUnitDal
    {
    }
}
