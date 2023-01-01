

using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

using Microsoft.Extensions.Configuration;
using Saas.Entities.Models.UserClaims;
using Saas.Core.Security.Security.Encyption;
using Saas.Entities.Models.User;

namespace Saas.Core.Security.Security.Jwt
{
    public class JwtHelper :ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //configuration.GetSection("TokenOptions") as TokenOptions;

        }

        public AccessToken CreateToken(CompanyUser user,List<CompanyOperationClaim> roles)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions,user,signingCredentials,roles);
            var jwtSecurityTakenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTakenHandler.WriteToken(jwt);
            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,
            CompanyUser user,SigningCredentials signingCredentials,List<CompanyOperationClaim> userOperationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user,userOperationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;

        }

        private IEnumerable<Claim> SetClaims(CompanyUser user,List<CompanyOperationClaim> rollsDetails)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.ID.ToString());
            claims.AddEmail(user.Email);
            claims.AddName(user.FullName);
            claims.AddUserOperationClaim(rollsDetails.Select(x => x.Name).ToArray());
            return claims;
        }
    }
}
