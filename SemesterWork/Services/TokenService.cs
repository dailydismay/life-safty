using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using SemesterWork.DAL.Domain;

namespace SemesterWork.Services
{
    public class TokenService
    {
        private string JWT_SECRET = "TW9zaGVFcmV6UHJpdmF0ZUtleQ==";

        public string GenerateToken(JwtAuthPayload payload)
        {
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(payload.Claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(payload.ExpireMinutes)),
                SigningCredentials = new SigningCredentials(GetSecretKey(JWT_SECRET), payload.SecurityAlgorithm)
            };

            var handler = new JwtSecurityTokenHandler();
            var sectoken = handler.CreateToken(descriptor);

            return handler.WriteToken(sectoken);
        }

        public int GetUserIdFromJwt(string token)
        {
            var paramz = GetTokenValidationParameters(JWT_SECRET);
            var securityHandler = new JwtSecurityTokenHandler();
            var tokenValid = securityHandler.ValidateToken(token, paramz, out SecurityToken validatedToken);

            return int.Parse(tokenValid.Claims.First(x => x.Type == "userId").Value);
        }

        public bool IsValid(string token)
        {
            try
            {
                var paramz = GetTokenValidationParameters(JWT_SECRET);
                var securityHandler = new JwtSecurityTokenHandler();
                var tokenValid = securityHandler.ValidateToken(token, paramz, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private SymmetricSecurityKey GetSecretKey(string secretKey)
        {
            var byteKey = Convert.FromBase64String(secretKey);

            return new SymmetricSecurityKey(byteKey);
        }

        private TokenValidationParameters GetTokenValidationParameters(string secretKey)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSecretKey(secretKey)
            };
        }
    }
}
