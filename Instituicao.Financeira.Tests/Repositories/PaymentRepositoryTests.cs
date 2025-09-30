using FluentAssertions;
using Intituicao.Financeira.Application.Shared.Repository;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Instituicao.Financeira.Tests.Repositories
{
    public class PaymentRepositoryTests
    {
        private IPaymentRepository _paymentRepository;
        private DbContextOptions<InstituicaoContexto> _options;

        public PaymentRepositoryTests()
        {
            var contrato = new DefaultFixture().ContratoMock(Guid.Parse("c2e6872e-913e-4282-bfcc-9be9a5f5cff1"));
            contrato.Pagamentos = null;

            _options = new DbContextOptionsBuilder<InstituicaoContexto>()
                .UseInMemoryDatabase(databaseName: "PaymentRepositoryTests")
                .Options;

            using (var context = new InstituicaoContexto(_options))
            {
                context.Database.EnsureDeleted();
                context.CondicaoVeiculos.Add(new DefaultFixture().CondicaoVeiculoMock());
                context.TipoVeiculos.Add(new DefaultFixture().TipoVeiculoMock());
                context.StatusPagamentos.Add(new DefaultFixture().StatusPagamentoMock());
                context.Contratos.Add(contrato);
                context.Pagamentos.Add(new DefaultFixture().PagamentoMock(Guid.Parse("b7202c2d-2d33-410e-9c73-b5b3dd6db9a4"), Guid.Parse("c2e6872e-913e-4282-bfcc-9be9a5f5cff1")));
                context.SaveChanges();
            }

            using (var context = new InstituicaoContexto(_options))
            {
                _paymentRepository = new PaymentRepository(context);
            }
        }

        [Fact]
        public async Task GetByContract_Should_GetByContractToDatabase()
        {
            using (var context = new InstituicaoContexto(_options))
            {
                _paymentRepository = new PaymentRepository(context);
                var payments = _paymentRepository.GetByContract(Guid.Parse("c2e6872e-913e-4282-bfcc-9be9a5f5cff1"));

                // Assert
                payments.Should().HaveCount(1);
                payments.Should().Contain(x => x.ContratoId == Guid.Parse("c2e6872e-913e-4282-bfcc-9be9a5f5cff1"));
            }
        }

        [Fact]
        public async Task GetByClient_Should_GetPaymentToDatabase()
        {
            using (var context = new InstituicaoContexto(_options))
            {
                _paymentRepository = new PaymentRepository(context);
                var payments = _paymentRepository.GetByClient(13456);

                // Assert
                payments.Should().HaveCount(1);
                payments.Should().Contain(x => x.Id == Guid.Parse("b7202c2d-2d33-410e-9c73-b5b3dd6db9a4"));
            }
        }

        [Fact]
        public async Task InsertContract_Should_InsertContractToDatabase()
        {
            // Arrange
            var expected = new DefaultFixture().PagamentoMock(Guid.Parse("53884ada-4869-43eb-b4e9-f7f4589942f6"), Guid.Parse("06f6a564-ceee-4ca2-80c5-2ba434d5a8e5"));

            using (var context = new InstituicaoContexto(_options))
            {
                _paymentRepository = new PaymentRepository(context);
                var payments = _paymentRepository.Insert(expected);

                // Assert
                payments.Should().NotBeNull();
                payments.Should().BeEquivalentTo(expected);
            }
        }
    }
}
