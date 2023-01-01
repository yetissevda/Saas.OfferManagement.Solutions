using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saas.Business.Abstract.Invoice;
using Saas.Business.ValidationRules.FluentValidation;
using Saas.Core.Aspect.Autofac.Caching;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.Aspect.Autofac.Transaction;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Saas.Core.CrossCuttingConcerns.Validation;
using Saas.Core.Utilities.Results;
using Saas.DataAccess.EntityFrameWorkCore.IDal.Invoices;
using Saas.Entities.Dto;
using Saas.Entities.Generic;
using Saas.Entities.Models;
using Saas.Entities.Models.Invoices.Header;
using Saas.Entities.Models.Invoices.Rows;

namespace Saas.Business.Concrete.Invoice
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyOfferRowsManager : ICompanyOfferRowsService
    {
        private ICompanyOfferRowDal _companyOfferRowDal;
        private ICompanyOfferDal _companyOfferDal;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyOfferRowDal"></param>
        public CompanyOfferRowsManager(ICompanyOfferRowDal companyOfferRowDal, ICompanyOfferDal companyOfferDal)
        {
            _companyOfferRowDal = companyOfferRowDal;
            _companyOfferDal = companyOfferDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(duration: 10)]
        public IDataResult<List<OfferRow>> GetCompanyOfferList()
        {
            return new SuccessDataResult<List<OfferRow>>(_companyOfferRowDal.GetList());
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<List<OfferRow>> GetCompanyOfferRowByHeaderId(Guid offerHeaderId)
        {
            var result = _companyOfferRowDal.GetList(x => x.HeaderId == offerHeaderId);
            return new SuccessDataResult<List<OfferRow>>(result);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Add(OfferRow offerRow)
        {
            ValidationTool.Validate(new OfferRowValidator(), offerRow);
            _companyOfferRowDal.Add(offerRow);
            return new SuccessResult();
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(OfferRow offerRow)
        {
            _companyOfferRowDal.Delete(offerRow);
            return new SuccessResult();
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Update(OfferRow offerRow)
        {
            _companyOfferRowDal.Delete(offerRow);
            return new SuccessResult();
        }
        [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(duration: 10)]
        public async Task<IDataResult<List<OfferRow>>> GetCompanyOfferRowsListAsync()
        {
            var res = await _companyOfferRowDal.GetAllAsync();
            return new SuccessDataResult<List<OfferRow>>(res.ToList());
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<List<OfferRow>>> GetCompanyOfferRowByHeaderIdAsync(Guid offerHeaderId)
        {
            var result = await _companyOfferRowDal.GetAllAsync();
            var filtered = result.Where(x => x.HeaderId == offerHeaderId).ToList();
            return new SuccessDataResult<List<OfferRow>>(filtered);
        }
        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IResult> AddAsync(OfferRow offerRow)
        {
            await Task.Run(() => ValidationTool.Validate(new OfferRowValidator(), offerRow));
            await _companyOfferRowDal.AddAsyn(offerRow);
            return new SuccessResult();
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IResult> DeleteAsync(OfferRow offerRow)
        {
            await _companyOfferRowDal.DeleteAsyn(offerRow);
            return new SuccessResult();
        }
        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IResult> UpdateAsync(OfferRow offerRow)
        {
            await _companyOfferRowDal.UpdateAsyn(offerRow, offerRow.ID);
            return new SuccessResult();
        }

        public IDataResult<IDto> SqlHelper(string query)
        {
            throw new NotImplementedException();
        }
        [TransactionScopeAspect]
        public async Task<IDataResult<CompanyOffer>> Approve(ApproveRequestDto request)
        {
            var lstOfRow = await _companyOfferRowDal.GetAllAsync();
            var approvedList = new List<OfferRow>();
            if (request.RowIdList != null && request.RowIdList.Count > 0)
            {
                Guid headerId = Guid.Empty;
                foreach (var item in request.RowIdList)
                {
                    var result = lstOfRow.FirstOrDefault(x => x.ID == item);
                    result.InvoiceApproveType = request.approve;
                    result.ApproveDate = DateTime.UtcNow;
                    _companyOfferRowDal.Update(result, result.ID);
                    headerId = result.HeaderId;
                }
                if (headerId != Guid.Empty) return new ErrorDataResult<CompanyOffer>("Not Found");
                {
                    var offer = await _companyOfferDal.FindAsync(x => x.ID == headerId);
                    return new SuccessDataResult<CompanyOffer>(offer);
                }
            }
            if (request.HeaderId == Guid.Empty) return new ErrorDataResult<CompanyOffer>("Not Found");
            {
                var offer = await _companyOfferDal.FindAsync(x => x.ID == request.HeaderId);
                var rows = await _companyOfferRowDal.FindAllAsync(x => x.HeaderId == request.HeaderId);
                foreach (var offerRow in rows)
                {
                    offerRow.InvoiceApproveType = request.approve;
                    offerRow.ApproveDate = DateTime.UtcNow;
                    _companyOfferRowDal.Update(offerRow, offerRow.ID);
                }

                return new SuccessDataResult<CompanyOffer>(offer);
            }
        }
    }
}
