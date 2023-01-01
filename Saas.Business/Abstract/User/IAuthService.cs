using System.Threading.Tasks;
using Saas.Core.Security.Security.Jwt;
using Saas.Core.Utilities.Results;
using Saas.Entities.Dto;
using Saas.Entities.Generic;
using Saas.Entities.Models.User;

namespace Saas.Business.Abstract.User
{
    public interface IAuthService
    {
        IDataResult<CompanyUser> Register(CompanyUserDto userForRegisterDto);
        IDataResult<CompanyUser> Login(UserForLoginDto userForLoginDto);

        IResult RegisterForCompany(CompanyFirstRegisterDto userForRegisterDto);
        IResult UserExist(string email);
        IDataResult<AccessToken> CreateAccessToken(CompanyUser user);

        IDataResult<IDto> SqlHelper(string query);

    }
}
