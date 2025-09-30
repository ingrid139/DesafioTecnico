using Intituicao.Financeira.Application.Shared.Core;

namespace Intituicao.Financeira.Application.Features.Clients.GetSummary.Models
{
    public class GetSummaryInput : IRequest<GetSummaryOutput>
    {
        public long CpfCnpj { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
