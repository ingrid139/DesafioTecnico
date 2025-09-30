namespace Intituicao.Financeira.Application.Features.Contracts.GetContract.Models
{
    public class GetContractOutput
    {
        public Guid Id { get; set; }
        public long ClienteCpfCnpj { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TaxaMensal { get; set; }
        public int PrazoMeses { get; set; }
        public DateTime DataVencimentoPrimeiraParcela { get; set; }
        public int TipoVeiculoId { get; set; }
        public int CondicaoVeiculoId { get; set; }
    }
}
