using Intituicao.Financeira.Application.Shared.Core;

namespace Intituicao.Financeira.Application.Features.Contracts.DeleteContract.Models
{
    public class DeleteContractInput : IRequest<DeleteContractOutput>
    {
        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
