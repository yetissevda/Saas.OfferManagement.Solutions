using Saas.Core.Utilities.Results;
using Saas.Entities.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saas.Business.Abstract.Product
{
    public interface ICompanyProductUnitService
    {
        IDataResult<List<CompanyProductUnits>> GetCompanyProductUnitList();
        IDataResult<CompanyProductUnits> GetCompanyProductUnitById(Guid productunitId);
        IResult Add(CompanyProductUnits productunit);
        IResult Delete(CompanyProductUnits productunit);
        IResult Update(CompanyProductUnits productunit);

        Task<IDataResult<List<CompanyProductUnits>>> GetCompanyProductUnitListAsync();
        Task<IDataResult<CompanyProductUnits>> GetCompanyProductUnitByIdAsync(Guid productunitId);
        Task<IDataResult<CompanyProductUnits>> AddAsync(CompanyProductUnits productunit);
        Task<IResult> DeleteAsync(CompanyProductUnits productunit);
        Task<IDataResult<CompanyProductUnits>> UpdateAsync(CompanyProductUnits productunit);
    }
}
