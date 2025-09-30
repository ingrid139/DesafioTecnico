using Intituicao.Financeira.Application.Shared.Services;

namespace Instituicao.Financeira.Tests.Services
{
    public class PriceCalculatorServiceTests
    {
        private readonly PriceCalculatorService _service;
        public PriceCalculatorServiceTests()
        {
            _service = new PriceCalculatorService();
        }
       
        [Theory]
        [InlineData(10, 1000, 904.42)]
        [InlineData(9, 904.42, 807.88)]
        [InlineData(8, 807.88, 710.38)]
        [InlineData(7, 710.38, 611.90)]
        [InlineData(6, 611.90, 512.44)]
        [InlineData(5, 512.44, 411.98)]
        [InlineData(4, 411.98, 310.52)]
        [InlineData(3, 310.52, 208.04)]
        [InlineData(2, 208.04, 104.54)]
        [InlineData(1, 104.54, 0)]
        public void PriceCalculatorService_Should_Be_ReturnExpected(int meses, decimal valor, decimal saldoDevedorExpected)
        {
            //Arrange
            decimal juros = 0.01M;

            //Act
            var calculo = _service.CalcularSaldoDevedor(valor, juros, meses);

            //Assert 
            Assert.Equal(saldoDevedorExpected, calculo);
        }
    }
}
