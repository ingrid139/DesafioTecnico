namespace Intituicao.Financeira.Application.Shared.Domain.Entities
{
    public class Contrato
    {
        public Guid Id { get; set; }
        public long ClienteCpfCnpj { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TaxaMensal { get; set; }
        public int PrazoMeses { get; set; }
        public DateTime DataVencimentoPrimeiraParcela { get; set; }
        public int TipoVeiculo { get; set; }
        public int CondicaoVeiculo { get; set; }
    }
}
