namespace Intituicao.Financeira.Application.Shared.Services
{
    public interface IPriceCalculatorService
    {
        decimal CalcularSaldoDevedor(decimal valor, decimal jurosMensal, int meses);
    }
}