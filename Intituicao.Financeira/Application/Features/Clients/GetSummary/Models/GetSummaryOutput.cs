namespace Intituicao.Financeira.Application.Features.Clients.GetSummary.Models
{
    public class GetSummaryOutput
    {
        public int QuantidadeContratos { get; private set; }
        public int ParcelasEmDia { get; private set; }
        public int ParcelasEmAtraso { get; private set; }
        public int ParcelasAVencer { get; private set; }
        public decimal PagasEmDia { get; private set; }
        public decimal SaldoDevedorConsolidado { get; private set; }

        public GetSummaryOutput(int quantidadeContratos, int parcelasEmDia, int parcelasEmAtraso, int parcelasAVencer, decimal pagasEmDia, decimal saldoDevedorConsolidado)
        {
            QuantidadeContratos = quantidadeContratos;
            ParcelasEmDia = parcelasEmDia;
            ParcelasEmAtraso = parcelasEmAtraso;
            ParcelasAVencer = parcelasAVencer;
            PagasEmDia = pagasEmDia;
            SaldoDevedorConsolidado = saldoDevedorConsolidado;
        }
    }
}
