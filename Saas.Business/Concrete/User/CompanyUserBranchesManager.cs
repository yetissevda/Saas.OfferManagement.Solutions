using Saas.Business.Abstract.User;
using Saas.Core.Aspect.Autofac.Caching;
using Saas.Core.Aspect.Autofac.Logging;
using Saas.Core.Aspect.Autofac.Performance;
using Saas.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Saas.Core.Utilities.Results;
using Saas.DataAccess.EntityFrameWorkCore.IDal;
using Saas.Entities.Generic;
using Saas.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saas.Business.Concrete.User
{
    public class CompanyUserBranchesManager : ICompanyUserBranchesService
    {
        private readonly ICompanyUserBranchesDal _companyUserBranches;


        public CompanyUserBranchesManager(ICompanyUserBranchesDal companyUserBranchesDal)
        {
            _companyUserBranches = companyUserBranchesDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        public Task<IDataResult<CompanyUserBranches>> Add(CompanyUserBranches userBranch)
        {
            _companyUserBranches.Add(userBranch);
            return Task.FromResult<IDataResult<CompanyUserBranches>>(new SuccessDataResult<CompanyUserBranches>(userBranch));
        }


        [LogAspect(typeof(DatabaseLogger))]
        public Task<IDataResult<CompanyUserBranches>> Delete(CompanyUserBranches userBranch)
        {

            _companyUserBranches.Delete(userBranch);
            return Task.FromResult<IDataResult<CompanyUserBranches>>(new SuccessDataResult<CompanyUserBranches>(userBranch));
        }

        [PerformanceAspect(interval: 5)]
        public Task<IDataResult<CompanyUserBranches>> GetCompanyUserBranchesById(Guid brancheId)
        {
            return Task.FromResult<IDataResult<CompanyUserBranches>>(new SuccessDataResult<CompanyUserBranches>(_companyUserBranches.Get(filter: p => p.ID == brancheId)));
        }

        [CacheAspect(duration: 10)]  //10 dakika boyunca cache te sonra db den tekrar cache e seklinde bir dongu
        [LogAspect(typeof(DatabaseLogger))]

        public Task<IDataResult<List<CompanyUserBranches>>> GetCompanyUserBranchesList()
        {
            return Task.FromResult<IDataResult<List<CompanyUserBranches>>>(new SuccessDataResult<List<CompanyUserBranches>>(_companyUserBranches.GetList()));
        }

        public IDataResult<IDto> SqlHelper(string query)
        {
            throw new NotImplementedException();
        }
        [LogAspect(typeof(DatabaseLogger))]
        public Task<IDataResult<CompanyUserBranches>> Update(CompanyUserBranches userBranch)
        {
            _companyUserBranches.Update(userBranch, userBranch.ID);
            return Task.FromResult<IDataResult<CompanyUserBranches>>(new SuccessDataResult<CompanyUserBranches>(userBranch));
        }
    }
}
