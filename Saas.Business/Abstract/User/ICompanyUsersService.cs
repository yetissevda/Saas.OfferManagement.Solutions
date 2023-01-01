using System;
using System.Collections.Generic;
using Saas.Core.Utilities.Results;
using Saas.Entities.Dto;
using Saas.Entities.Generic;
using Saas.Entities.Models.User;
using Saas.Entities.Models.UserClaims;

namespace Saas.Business.Abstract.User
{
    public interface ICompanyUserService
    {
        List<CompanyOperationClaim> GetClaims(CompanyUser user);
        IDataResult<List<CompanyUser>> GetUserList();
        IDataResult<CompanyUser> GetUserById(Guid userId);
        CompanyUser GetByMail(string mail);
        IDataResult<CompanyUser> Register(CompanyUserDto user);
        IResult Delete(CompanyUser user);
        IDataResult<CompanyUser> UpdateUser(CompanyUserUpdateDto user);
        IResult Update(CompanyUser user);

        IDataResult<IDto> SqlHelper(string query);

    }
}
