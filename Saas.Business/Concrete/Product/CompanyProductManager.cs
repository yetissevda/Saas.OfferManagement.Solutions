using Saas.Business.Abstract.Product;
using Saas.Core.Aspect.Autofac.Caching;
using Saas.Core.Utilities.Results;
using Saas.DataAccess.EntityFrameWorkCore.IDal.Product;
using Saas.Entities.Generic;
using Saas.Entities.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace Saas.Business.Concrete.Product
{
    public class CompanyProductManager : ICompanyProductService
    {
        private ICompanyProductDal _companyProductDal;

        public CompanyProductManager(ICompanyProductDal companyProductDal)
        {
            _companyProductDal = companyProductDal;
        }

        [CacheAspect(duration: 10)]
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<CompanyProducts>> GetCompanyProductList()
        {
            var res = _companyProductDal.GetList();
            return new SuccessDataResult<List<CompanyProducts>>(res);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<CompanyProducts> GetCompanyProductById(Guid productId)
        {
            var pro = _companyProductDal.Get(x => x.ID == productId);
            if (pro is null)
            {
                return new ErrorDataResult<CompanyProducts>("Product not found");
            }
            return new SuccessDataResult<CompanyProducts>(pro);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Add(CompanyProducts product)
        {
            _companyProductDal.Add(product);
            return new SuccessResult("");

        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(CompanyProducts product)
        {
            _companyProductDal.Delete(product);
            return new SuccessResult("");
        }

        public IResult Update(CompanyProducts product)
        {
            _companyProductDal.Update(product, product.ID);
            return new SuccessResult("");
        }

        [CacheAspect(duration: 10)]
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<List<CompanyProducts>>> GetCompanyProductListAsync()
        {
            var res = await _companyProductDal.GetAllAsync();
            return new SuccessDataResult<List<CompanyProducts>>(res.ToList());
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<CompanyProducts>> GetCompanyProductByIdAsync(Guid productId)
        {
            var res = await _companyProductDal.FindAsync(x => x.ID == productId);
            if (res is null)
            {
                return new ErrorDataResult<CompanyProducts>("Product not found");
            }

            return new SuccessDataResult<CompanyProducts>(res);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<CompanyProducts>> AddAsync(CompanyProducts product)
        {
            await _companyProductDal.AddAsyn(product);
            return new SuccessDataResult<CompanyProducts>(product);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IResult> DeleteAsync(CompanyProducts product)
        {
            await _companyProductDal.DeleteAsyn(product);
            return new SuccessDataResult<CompanyProducts>();
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<CompanyProducts>> UpdateAsync(CompanyProducts product)
        {
            await _companyProductDal.UpdateAsyn(product, product.ID);
            return new SuccessDataResult<CompanyProducts>(product);
        }

        public IDataResult<IDto> SqlHelper(string query)
        {
            throw new NotImplementedException();
        }
    }
}
