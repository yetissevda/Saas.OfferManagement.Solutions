using Microsoft.IdentityModel.Tokens;

namespace Saas.Core.Security.Security.Encyption
{
    public static class SigningCredentialsHelper
    {


        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384);
        }
    }
}
