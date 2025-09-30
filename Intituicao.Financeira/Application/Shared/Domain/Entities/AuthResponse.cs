using System.Diagnostics.CodeAnalysis;

namespace Intituicao.Financeira.Application.Shared.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
    }
}
