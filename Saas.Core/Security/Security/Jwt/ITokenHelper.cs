using Saas.Entities.Models.User;
using Saas.Entities.Models.UserClaims;
using System.Collections.Generic;


namespace Saas.Core.Security.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(CompanyUser user,List<CompanyOperationClaim> roles);
    }
}
