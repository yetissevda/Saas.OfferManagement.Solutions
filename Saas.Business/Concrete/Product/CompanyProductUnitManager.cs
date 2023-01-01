using Saas.Business.Abstract.Product;
using Saas.Business.ValidationRules.FluentValidation;
using Saas.Core.Aspect.Autofac.Caching;
using Saas.Core.CrossCuttingConcerns.Validation;
using Saas.Core.Utilities.Results;
using Saas.DataAccess.EntityFrameWorkCore.IDal.Product;
using Saas.Entities.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace Saas.Business.Concrete.Product
{
    public class CompanyProductUnitManager : ICompanyProductUnitService
    {
        private ICompanyProductUnitDal _companyProductUnitDal;

        public CompanyProductUnitManager(ICompanyProductUnitDal companyProductUnitDal)
        {
            _companyProductUnitDal = companyProductUnitDal;
        }
        
        [CacheAspect(duration: 10)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<CompanyProductUnits>> GetCompanyProductUnitList()
        {
            var res = _companyProductUnitDal.GetList();
            return new SuccessDataResult<List<CompanyProductUnits>>(res);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<CompanyProductUnits> GetCompanyProductUnitById(Guid productunitId)
        {
            var res = _companyProductUnitDal.Get(x => x.ID == productunitId);
            if (res is null)
                return new ErrorDataResult<CompanyProductUnits>("Not Found");
            return new SuccessDataResult<CompanyProductUnits>(res);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Add(CompanyProductUnits productunit)
        {
            ValidationTool.Validate(new ProductUnitValidator(), productunit);
            _companyProductUnitDal.Add(productunit);
            return new SuccessDataResult<CompanyProductUnits>(productunit);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(CompanyProductUnits productunit)
        {
            _companyProductUnitDal.Delete(productunit);
            return new SuccessDataResult<CompanyProductUnits>();
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Update(CompanyProductUnits productunit)
        {
            _companyProductUnitDal.Update(productunit, productunit.ID);
            return new SuccessDataResult<CompanyProductUnits>(productunit);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(duration: 10)]
        public async Task<IDataResult<List<CompanyProductUnits>>> GetCompanyProductUnitListAsync()
        {
            var es = await _companyProductUnitDal.GetAllAsync();
            return new SuccessDataResult<List<CompanyProductUnits>>(es.ToList());
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<CompanyProductUnits>> GetCompanyProductUnitByIdAsync(Guid productunitId)
        {
            var es = await _companyProductUnitDal.GetAsync(productunitId);
            if (es is null)
                return new ErrorDataResult<CompanyProductUnits>("Not Found");
            return new SuccessDataResult<CompanyProductUnits>(es);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<CompanyProductUnits>> AddAsync(CompanyProductUnits productunit)
        {
            await Task.Run(() => ValidationTool.Validate(new ProductUnitValidator(), productunit));
            await _companyProductUnitDal.AddAsyn(productunit);
            return new SuccessDataResult<CompanyProductUnits>(productunit);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IResult> DeleteAsync(CompanyProductUnits productunit)
        {
            await _companyProductUnitDal.DeleteAsyn(productunit);
            return new SuccessResult();
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<CompanyProductUnits>> UpdateAsync(CompanyProductUnits productunit)
        {
            await _companyProductUnitDal.UpdateAsyn(productunit, productunit.ID);
            return new SuccessDataResult<CompanyProductUnits>(productunit);
        }
    }
}
