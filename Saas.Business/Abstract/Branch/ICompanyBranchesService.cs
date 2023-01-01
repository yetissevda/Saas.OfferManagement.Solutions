
using System;
using System.Collections.Generic;

using Saas.Core.Utilities.Results;
using Saas.Entities.Generic;
using Saas.Entities.Models.Branch;

namespace Saas.Business.Abstract.Branch
{
    public interface ICompanyBranchesService
    {

        IDataResult<List<CompanyBranch>> CompanyBranchesList();
        IDataResult<CompanyBranch> CompanyBranchById(Guid userId);

        IResult Add(CompanyBranch companyBranch);
        IResult Delete(CompanyBranch companyBranch);
        IResult Update(CompanyBranch companyBranch);

        IDataResult<IDto> SqlHelper(string query);

    }
}
