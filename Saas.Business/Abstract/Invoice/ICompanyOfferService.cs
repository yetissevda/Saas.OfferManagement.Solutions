using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Core.Utilities.Results;
using Saas.Entities.Generic;
using Saas.Entities.Models;
using Saas.Entities.Models.Invoices.Header;

namespace Saas.Business.Abstract.Invoice
{
    public interface ICompanyOfferService
    {
        IDataResult<List<CompanyOffer>> GetCompanyOfferList();
        IDataResult<CompanyOffer> GetCompanyOfferById(Guid Id);
        IResult Add(CompanyOffer offer);
        IResult Delete(CompanyOffer offer);
        IResult Update(CompanyOffer offer);

        Task<IDataResult<List<CompanyOffer>>> GetCompanyOfferListAsync();
        Task<IDataResult<CompanyOffer>> GetCompanyOfferByIdAsync(Guid offerId);
        Task<IDataResult<CompanyOffer>> AddAsync(CompanyOffer offer);
        Task<IResult> DeleteAsync(CompanyOffer offer);
        Task<IDataResult<CompanyOffer>> UpdateAsync(CompanyOffer offer);

        IDataResult<IDto> SqlHelper(string query);
    }
}
