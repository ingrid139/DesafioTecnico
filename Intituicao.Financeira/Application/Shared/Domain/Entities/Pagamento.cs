using Intituicao.Financeira.Application.Shared.Domain.Enum;

namespace Intituicao.Financeira.Application.Shared.Domain.Entities
{
    public class Pagamento
    {
        public Guid Id { get; private set; }
        public Guid ContratoId { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public DateTime DataPagamento { get; private set; }
        public int StatusPagamentoId { get; private set; }
        public decimal SaldoDevedor { get; private set; }

        public Pagamento(Guid id, Guid contratoId)
        {
            Id = id;
            ContratoId = contratoId;
        }

        public void SetDataVencimento(DateTime dataAnterior)
        {
            this.DataVencimento = dataAnterior.AddDays(30);
        }

        public void SetPrimeiraDataVencimento(DateTime dataAnterior)
        {
            this.DataVencimento = dataAnterior;
        }
        public void SetDataStatusPagemento()
        {
            this.DataPagamento = DateTime.UtcNow;
            this.StatusPagamentoId = DataPagamento <= DataVencimento ? (int)PaymentStatus.EmDia : (int)PaymentStatus.EmAtraso;
        }

        public void SetSaldoDevedor(decimal saldoDevedor)
        {
            this.SaldoDevedor = saldoDevedor;
        }
    }
}
