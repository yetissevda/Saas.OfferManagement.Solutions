using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.DataAccess.EntityFrameWorkCore.DbContexts;
using Saas.DataAccess.EntityFrameWorkCore.IDal.Invoices;
using Saas.Entities.Generic;
using Saas.Entities.Models.Invoices.Header;
using Saas.Entities.Models.Invoices.Rows;

namespace Saas.DataAccess.EntityFrameWorkCore.EfDal.Invoices
{
    public class EfCompanyOfferRowsDal : EfEntityRepositoryBase<OfferRow, GordionDbContext>, ICompanyOfferRowDal
    {

    }

}
