using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Saas.Core.Utilities.Results;
using Saas.Entities.Generic;
using Saas.Entities.Models.User;

namespace Saas.Business.Abstract.User
{
    public interface ICompanyUserBranchesService
    {
        Task<IDataResult<List<CompanyUserBranches>>> GetCompanyUserBranchesList();
        Task<IDataResult<CompanyUserBranches>> GetCompanyUserBranchesById(Guid branchId);
        Task<IDataResult<CompanyUserBranches>> Add(CompanyUserBranches company);
        Task<IDataResult<CompanyUserBranches>> Delete(CompanyUserBranches company);
        Task<IDataResult<CompanyUserBranches>> Update(CompanyUserBranches company);

        IDataResult<IDto> SqlHelper(string query);
    }
}
