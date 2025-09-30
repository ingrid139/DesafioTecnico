namespace Intituicao.Financeira.Application.Shared.Services
{
    public class PriceCalculatorService : IPriceCalculatorService
    {

        public decimal CalcularSaldoDevedor(decimal valor, decimal jurosMensal, int meses)
        {
            decimal parcelaFixa = CalcularParcelaFixa(valor, jurosMensal, meses);
            decimal juros = valor * jurosMensal;
            decimal amortizacao = parcelaFixa - juros;
            decimal saldo = valor - amortizacao;

            return Math.Round(saldo, 2);
        }

        private decimal CalcularParcelaFixa(decimal valor, decimal jurosMensal, int meses)
        {
            decimal fator = (decimal)Math.Pow((double)(1 + jurosMensal), meses);
            return valor * jurosMensal * fator / (fator - 1);
        }
    }
}
