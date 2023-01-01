using System.IdentityModel.Tokens.Jwt;

namespace Saas.WebCoreApi.Helpers
{
    /// <summary>
    /// Parse Token From Client
    /// </summary>
    public static class TokenHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static string ParseUserNameFromAccessToken(HttpClient client)
        {
            string userName = null;
            var headers = client.DefaultRequestHeaders.ToDictionary(a => a.Key, a => string.Join(";", a.Value));
            foreach (var header in headers)
            {
                if (header.Key.ToLower().Equals("authorization")) // do not log authorization header
                {
                    var authorizationValue = header.Value;
                    if (authorizationValue.StartsWith("Bearer"))
                    {
                        var jwt = authorizationValue.Replace("Bearer", string.Empty).Trim();
                        if (!string.IsNullOrEmpty(jwt))
                        {
                            var handler = new JwtSecurityTokenHandler();
                            var tokenS = handler.ReadToken(jwt) as JwtSecurityToken;

                            foreach (var claim in tokenS.Claims)
                            {
                                var claimValue = claim.Value;
                                if (claim.Type == "email")
                                {
                                    userName = claim?.Value;
                                    break;
                                }
                            }
                        }
                    }
                }

            }
            return userName;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string ParseUserNameFromAccessToken(string token)
        {
            string userName = null;
            if (token.StartsWith("Bearer"))
            {
                var jwt = token.Replace("Bearer", string.Empty).Trim();
                if (!string.IsNullOrEmpty(jwt))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var tokenS = handler.ReadToken(jwt) as JwtSecurityToken;

                    foreach (var claim in tokenS.Claims)
                    {
                        var claimValue = claim.Value;
                        if (claim.Type == "email")
                        {
                            userName = claim?.Value;
                            break;
                        }
                    }
                }
            }
            return userName;
        }
    }
}
