using Saas.Entities.Generic;
using Saas.Entities.Models.Invoices.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Entities.Models.Invoices.Rows;

namespace Saas.DataAccess.EntityFrameWorkCore.IDal.Invoices
{
    public interface ICompanyOfferRowDal : IEntityRepository<OfferRow>, IEntityRepositoryAsync<OfferRow>
    {
    }

}
