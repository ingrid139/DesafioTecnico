using Intituicao.Financeira.Application.Shared.Core;

namespace Intituicao.Financeira.Application.Features.Contracts.GetContract.Models
{
    public class GetContractInput : IRequest<GetContractOutput>
    {
        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
