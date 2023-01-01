using Saas.DataAccess.EntityFrameWorkCore.DbContexts;
using Saas.DataAccess.EntityFrameWorkCore.IDal.Invoices;
using Saas.Entities.Generic;
using Saas.Entities.Models.Invoices.Rows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.DataAccess.EntityFrameWorkCore.IDal.Product;
using Saas.Entities.Models.Products;

namespace Saas.DataAccess.EntityFrameWorkCore.EfDal.Product
{
    public class EfCompanyProductDal: EfEntityRepositoryBase<CompanyProducts, GordionDbContext>, ICompanyProductDal
    {

    }
    
}
