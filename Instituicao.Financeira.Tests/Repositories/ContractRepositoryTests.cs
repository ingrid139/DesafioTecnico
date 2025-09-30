using FluentAssertions;
using Intituicao.Financeira.Application.Shared.Repository;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Instituicao.Financeira.Tests.Repositories
{
    public class ContractRepositoryTests
    {
        private IContractRepository _ContractRepository;

        [Fact]
        public async Task GetByClient_Should_GetPaymentToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<InstituicaoContexto>()
                        .UseInMemoryDatabase(databaseName: "CatalogoListDatabase")
                        .Options;

            using (var context = new InstituicaoContexto(options))
            {
                context.Database.EnsureDeleted();
                context.CondicaoVeiculos.Add(new DefaultFixture().CondicaoVeiculoMock());
                context.TipoVeiculos.Add(new DefaultFixture().TipoVeiculoMock());
                context.Contratos.Add(new DefaultFixture().ContratoMock(Guid.Parse("6604faea-9f25-4afa-ba6c-00404a79a079")));
                context.SaveChanges();
            }

            using (var context = new InstituicaoContexto(options))
            {
                _ContractRepository = new ContractRepository(context);
                var payments = _ContractRepository.GetByClient(13456);

                // Assert
                payments.Should().HaveCount(1);
                payments.Should().Contain(x => x.ClienteCpfCnpj == 13456);
            }
        }

        [Fact]
        public async Task GetById_Should_GetByIdToDatabase()
        {
            // Arrange
            var expected = new DefaultFixture().ContratoMock(Guid.Parse("893ff30f-b77e-4450-9f93-17aaa4de3d95"));
            expected.Pagamentos = null;

            var options = new DbContextOptionsBuilder<InstituicaoContexto>()
                        .UseInMemoryDatabase(databaseName: "CatalogoListDatabase")
                        .Options;

            using (var context = new InstituicaoContexto(options))
            {
                context.Database.EnsureDeleted();
                context.CondicaoVeiculos.Add(new DefaultFixture().CondicaoVeiculoMock());
                context.TipoVeiculos.Add(new DefaultFixture().TipoVeiculoMock());
                context.Contratos.Add(expected);
                context.SaveChanges();
            }

            using (var context = new InstituicaoContexto(options))
            {
                _ContractRepository = new ContractRepository(context);
                var contrato = _ContractRepository.GetById(Guid.Parse("893ff30f-b77e-4450-9f93-17aaa4de3d95"));

                // Assert
                contrato.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async Task InsertContract_Should_InsertContractToDatabase()
        {
            // Arrange
            var expected = new DefaultFixture().ContratoMock(Guid.Parse("dfe80abd-3a8f-4b13-8955-6ef28ad6ce6a"));
            var options = new DbContextOptionsBuilder<InstituicaoContexto>()
                        .UseInMemoryDatabase(databaseName: "CatalogoListDatabase")
                        .Options;

            using (var context = new InstituicaoContexto(options))
            {
                context.Database.EnsureDeleted();
                _ContractRepository = new ContractRepository(context);
                var payments = _ContractRepository.Insert(expected);

                // Assert
                payments.Should().NotBeNull();
                payments.Should().BeEquivalentTo(expected);
            }
        }
    }
}
