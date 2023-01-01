namespace Saas.Core.Security.Security.Jwt
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        /// <summary>
        /// Minutes 
        /// </summary>
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
