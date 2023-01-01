using Saas.Entities.Generic;
using Saas.Entities.Models.UserClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Entities.Models.Products;

namespace Saas.DataAccess.EntityFrameWorkCore.IDal.Product
{
    public interface ICompanyProductDal : IEntityRepository<CompanyProducts>, IEntityRepositoryAsync<CompanyProducts>
    {
    }
}
