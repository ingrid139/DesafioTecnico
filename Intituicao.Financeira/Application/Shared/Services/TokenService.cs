using Intituicao.Financeira.Application.Shared.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security;
using System.Text;

namespace Intituicao.Financeira.Application.Shared.Services
{
    [ExcludeFromCodeCoverage]
    public static class TokenService
    {
        private static string _client = "desafio-tecnico";
        private static string _secret = "80bd2450-d1f1-4b56-8f25-7569c7fa6c3b";
        public static string GenerateToken(SecureString secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(new NetworkCredential("", secret).Password);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static bool Validate(AuthRequest authRequest)
        {
            if (_client.Equals(authRequest.ClientId) && _secret.Equals(authRequest.ClientScret))
                return true;
            return false;
        }
    }
}
