using Saas.Business.Abstract.Companies;
using Saas.Business.Constants;
using Saas.Business.ValidationRules.FluentValidation;
using Saas.Core.Aspect.Autofac.Caching;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.Aspect.Autofac.Performance;
using Saas.Core.Aspect.Autofac.Validation;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Saas.Core.CrossCuttingConcerns.Validation;
using Saas.Core.Utilities.Business;
using Saas.Core.Utilities.Results;
using Saas.DataAccess.EntityFrameWorkCore.IDal;
using Saas.Entities.Generic;
using Saas.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Saas.Business.Concrete.Companies
{

    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal _companyDal;


        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }
        #region sync

        [CacheAspect(duration: 10)]  //10 dakika boyunca cache te sonra db den tekrar cache e seklinde bir dongu
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(interval: 5)]
        public IDataResult<List<Company>> GetCompanyList()
        {
            return new DataResult<List<Company>>(_companyDal.GetList(), true);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<Company> GetCompanyById(Guid companyId)
        {
            return new DataResult<Company>(_companyDal.Get(filter: p => p.ID == companyId), true);
        }

        [ValidationAspect(typeof(CompanyValidator), Priority = 1)] //add methoduna girmeden araya girip once kontrol saglar
        [LogAspect(typeof(DatabaseLogger))]
        public IResult Add(Company company)
        {
            // ValidationTool.Validate(new CompanyValidator(), company);
            IResult result = BusinessRules.Run(CheckCompanyTaxNumberExist(company.TaxNumber));
            if (result != null)
                return result;
            _companyDal.Add(company);
            return new DataResult<Company>(Messages.CompanyAdded);
        }

        private IResult CheckCompanyTaxNumberExist(string companyTaxNumber)
        {
            if (_companyDal.GetList(p => p.TaxNumber == companyTaxNumber).Count != 0)
            {
                return new ErrorResult(message: Messages.CompanyTaxNumberExistError);
            }
            return new SuccessResult();
        }

        [LogAspect(typeof(DatabaseLogger))]
        public IResult Delete(Company company)
        {
            _companyDal.Delete(company);

            return new DataResult<Company>(message: Messages.CompanyDeleted);
        }


        [LogAspect(typeof(DatabaseLogger))]
        public IResult Update(Company company)
        {
            _companyDal.Update(company, company.ID);

            return new SuccessDataResult<Company>(Messages.CompanyUpdated);
        }


        #endregion

        #region async


        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IResult> AddAsync(Company company)
        {
            ValidationTool.Validate(new CompanyValidator(), company);
            IResult result = BusinessRules.Run(await CheckCompanyTaxNumberExistAsymc(company.TaxNumber));
            if (result != null)
                return result;
            await _companyDal.AddAsyn(company);
            return new DataResult<Company>(Messages.CompanyAdded);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IResult> DeleteAsync(Company company)
        {
            company.Deleted = true;
            await _companyDal.UpdateAsyn(company, company.ID);
            return new DataResult<Company>(Messages.CompanyUpdated);
        }
        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IResult> UpdateAsync(Company company)
        {
            await _companyDal.UpdateAsyn(company, company.ID);
            return new DataResult<Company>(Messages.CompanyUpdated);
        }
        private async Task<IResult> CheckCompanyTaxNumberExistAsymc(string companyTaxNumber)
        {
            var result = await _companyDal.GetAllAsync();
            if (result.ToList().Any(x => x.TaxNumber == companyTaxNumber))
            {
                return new ErrorResult(message: Messages.CompanyTaxNumberExistError);
            }
            return new SuccessResult();
        }


        [CacheAspect(duration: 10)]
        [LogAspect(typeof(SeqAppender))]
        [PerformanceAspect(interval: 5)]
        public async Task<IDataResult<List<Company>>> GetCompanyListAsync()
        {
            ICollection<Company> companies = await _companyDal.GetAllAsync();
            var result = new DataResult<List<Company>>(companies.ToList(), true);
            return result;
        }

        [CacheAspect(duration: 10)]
        [LogAspect(typeof(DatabaseLogger))]

        public async Task<IDataResult<Company>> GetCompanyByIdAsync(Guid companyId)
        {
            var companies = await _companyDal.GetAsync(companyId);
            return new DataResult<Company>(companies, true);
        }

        public IDataResult<IDto> SqlHelper(string query)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
