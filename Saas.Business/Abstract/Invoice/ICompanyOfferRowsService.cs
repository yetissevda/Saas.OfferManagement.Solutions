using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Core.Utilities.Results;
using Saas.Entities.Dto;
using Saas.Entities.Generic;
using Saas.Entities.Models.Invoices.Header;
using Saas.Entities.Models.Invoices.Rows;

namespace Saas.Business.Abstract.Invoice
{
    public interface ICompanyOfferRowsService
    {
        IDataResult<List<OfferRow>> GetCompanyOfferList();
        IDataResult<List<OfferRow>> GetCompanyOfferRowByHeaderId(Guid headerId);
        IResult Add(OfferRow company);
        IResult Delete(OfferRow company);
        IResult Update(OfferRow company);

        Task<IDataResult<List<OfferRow>>> GetCompanyOfferRowsListAsync();
        Task<IDataResult<List<OfferRow>>> GetCompanyOfferRowByHeaderIdAsync(Guid companyId);
        Task<IResult> AddAsync(OfferRow company);
        Task<IResult> DeleteAsync(OfferRow company);
        Task<IResult> UpdateAsync(OfferRow company);

        IDataResult<IDto> SqlHelper(string query);

        Task<IDataResult<CompanyOffer>> Approve(ApproveRequestDto request);
    }
}
