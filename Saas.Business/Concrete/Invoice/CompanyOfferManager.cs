using Saas.Business.Abstract.Invoice;
using Saas.Core.Aspect.Autofac.Caching;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.Aspect.Autofac.Performance;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Saas.Core.Utilities.Results;
using Saas.DataAccess.EntityFrameWorkCore.IDal.Invoices;
using Saas.Entities.Generic;
using Saas.Entities.Models.Invoices.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saas.Business.Concrete.Invoice
{
    public class CompanyOfferManager : ICompanyOfferService
    {
        private ICompanyOfferDal _companyOfferDal;

        private ICompanyOfferRowDal _companyOfferRowDal;
        public CompanyOfferManager(ICompanyOfferDal companyOfferDal, ICompanyOfferRowDal companyOfferRowDal)
        {
            _companyOfferDal = companyOfferDal;
            _companyOfferRowDal = companyOfferRowDal;
        }

        [CacheAspect(duration: 10)]  //10 dakika boyunca cache te sonra db den tekrar cache e seklinde bir dongu
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(interval: 5)]

        public IDataResult<List<CompanyOffer>> GetCompanyOfferList()
        {
            return new SuccessDataResult<List<CompanyOffer>>(_companyOfferDal.GetList());
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<CompanyOffer> GetCompanyOfferById(Guid Id)
        {
            return new SuccessDataResult<CompanyOffer>(_companyOfferDal.Get(x => x.ID == Id));
        }

        [LogAspect(typeof(DatabaseLogger))]
        public IResult Add(CompanyOffer offer)
        {
            _companyOfferDal.Add(offer);
            foreach (var row in offer.Rows)
                _companyOfferRowDal.Add(row);
            return new SuccessResult();
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(CompanyOffer offer)
        {
            var rows = _companyOfferRowDal.GetList(x => x.HeaderId == offer.ID);
            _companyOfferDal.Delete(offer);
            foreach (var row in rows)
                _companyOfferRowDal.Delete(row);
            return new SuccessResult();
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Update(CompanyOffer offer)
        {
            _companyOfferDal.Update(offer, offer.ID);
            foreach (var row in offer.Rows)
                _companyOfferRowDal.Update(row, row.ID);
            return new SuccessResult();
        }

        [CacheAspect(duration: 10)]
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<List<CompanyOffer>>> GetCompanyOfferListAsync()
        {
            var result = await _companyOfferDal.GetAllAsync();
            return new SuccessDataResult<List<CompanyOffer>>(result.ToList());
        }

        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<CompanyOffer>> GetCompanyOfferByIdAsync(Guid companyOfferId)
        {
            var res = await _companyOfferDal.GetAsync(companyOfferId);
            if (res != null)
            {
                res.Rows = _companyOfferRowDal.GetList(x => x.HeaderId == companyOfferId);
                return new SuccessDataResult<CompanyOffer>(res);
            }
            return new ErrorDataResult<CompanyOffer>("Data Not Found");
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<CompanyOffer>> AddAsync(CompanyOffer offer)
        {
            var res = await _companyOfferDal.AddAsyn(offer);
            return new SuccessDataResult<CompanyOffer>(res);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IResult> DeleteAsync(CompanyOffer offer)
        {
            var list = _companyOfferRowDal.GetList(x => x.HeaderId == offer.ID);

            if (offer.Rows.Any())
            {
                foreach (var row in offer.Rows)
                    await _companyOfferRowDal.DeleteAsyn(row);
            }
            else
            {
                foreach (var row in list)
                    await _companyOfferRowDal.DeleteAsyn(row);
            }
            await _companyOfferDal.DeleteAsyn(offer);
            return new SuccessResult();
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<CompanyOffer>> UpdateAsync(CompanyOffer offer)
        {
            var res = await _companyOfferDal.UpdateAsyn(offer, offer.ID);
            res.Rows.ForEach(x => x.HeaderId = offer.ID);
            if (offer.Rows.Any())
                foreach (var row in offer.Rows)
                    await _companyOfferRowDal.UpdateAsyn(row, row.ID);
            return new SuccessDataResult<CompanyOffer>(offer);
        }

        public IDataResult<IDto> SqlHelper(string query)
        {
            return new DataResult<IDto>("");
        }
    }
}
