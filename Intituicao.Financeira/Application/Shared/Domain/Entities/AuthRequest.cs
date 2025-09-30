using System.Diagnostics.CodeAnalysis;

namespace Intituicao.Financeira.Application.Shared.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class AuthRequest
    {
        public string ClientId { get; set; }
        public string ClientScret { get; set; }
    }
}
