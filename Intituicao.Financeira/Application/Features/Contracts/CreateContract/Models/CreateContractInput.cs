using Intituicao.Financeira.Application.Shared.Core;

namespace Intituicao.Financeira.Application.Features.Contracts.CreateContract.Models
{
    public class CreateContractInput : IRequest<CreateContractOutput>
    {
        public Guid Id { get; set; }
        public long ClienteCpfCnpj { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TaxaMensal { get; set; }
        public int PrazoMeses { get; set; }
        public DateTime DataVencimentoPrimeiraParcela { get; set; }
        public int TipoVeiculoId { get; set; }
        public int CondicaoVeiculoId { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
