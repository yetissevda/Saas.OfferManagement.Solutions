using Saas.DataAccess.EntityFrameWorkCore.DbContexts;
using Saas.DataAccess.EntityFrameWorkCore.IDal.Invoices;
using Saas.Entities.Generic;
using Saas.Entities.Models;
using Saas.Entities.Models.Invoices.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.DataAccess.EntityFrameWorkCore.EfDal.Invoices
{
    public class EfCompanyOfferDal : EfEntityRepositoryBase<CompanyOffer, GordionDbContext>, ICompanyOfferDal
    {

    }

}
