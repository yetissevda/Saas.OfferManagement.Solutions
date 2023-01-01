using Saas.Core.Utilities.Results;
using Saas.Entities.Generic;
using Saas.Entities.Models.Invoices.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Entities.Models.Products;

namespace Saas.Business.Abstract.Product
{
    public interface ICompanyProductService
    {
        IDataResult<List<CompanyProducts>> GetCompanyProductList();
        IDataResult<CompanyProducts> GetCompanyProductById(Guid productId);
        IResult Add(CompanyProducts product);
        IResult Delete(CompanyProducts product);
        IResult Update(CompanyProducts product);

        Task<IDataResult<List<CompanyProducts>>> GetCompanyProductListAsync();
        Task<IDataResult<CompanyProducts>> GetCompanyProductByIdAsync(Guid productId);
        Task<IDataResult<CompanyProducts>> AddAsync(CompanyProducts product);
        Task<IResult> DeleteAsync(CompanyProducts product);
        Task<IDataResult<CompanyProducts>> UpdateAsync(CompanyProducts product);

        IDataResult<IDto> SqlHelper(string query);
    }
}
