using Intituicao.Financeira.Application.Shared.Core;

namespace Intituicao.Financeira.Application.Features.Payments.InsertPayment.Models
{
    public class InsertPaymentInput : IRequest<InsertPaymentOutput>
    {
        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
