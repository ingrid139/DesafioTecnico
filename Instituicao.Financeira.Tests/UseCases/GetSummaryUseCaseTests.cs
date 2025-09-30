using Intituicao.Financeira.Application.Shared.Core;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Intituicao.Financeira.Application.Shared.Repository.Models;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Intituicao.Financeira.Application.Features.Clients.GetSummary.Models;
using Intituicao.Financeira.Application.Features.Clients.GetSummary.UseCase;

namespace Instituicao.Financeira.Tests.UseCases
{
    public class GetSummaryUseCaseTests
    {
        private readonly Mock<ILogger<UseCaseHandlerBase<GetSummaryInput, GetSummaryOutput>>> _loggerMock;
        private readonly Mock<IContractRepository> _contractRepositoryMock;

        public GetSummaryUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<UseCaseHandlerBase<GetSummaryInput, GetSummaryOutput>>>();
            _contractRepositoryMock = new Mock<IContractRepository>();
        }

        [Fact]
        public async Task GetSummaryUseCaseTests_Should_Execute()
        {
            var input = new Fixture().Build<GetSummaryInput>()
                .With(x => x.CpfCnpj, 12346)
                .Create();

            var expected = new List<ContratoDto>() {
                new DefaultFixture().ContratoMock(Guid.Parse("b10af031-6b8c-408a-bbbe-509e1b903d4e")),
                new DefaultFixture().ContratoMock(Guid.Parse("b3488748-b1c5-4cd5-8106-ee93e260da06")),
            };

            _contractRepositoryMock.Setup(x => x.GetByClient(It.IsAny<long>()))
                .Returns(expected);

            var useCase = CreateInstance();
            var result = await useCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.GetResult().SaldoDevedorConsolidado.Should().Be(51000);
            result.GetResult().QuantidadeContratos.Should().Be(2);
            result.GetResult().ParcelasAVencer.Should().Be(15);
        }

        [Fact]
        public async Task GetSummaryUseCaseTests_Should_Execute_WithoutPayments()
        {
            var input = new Fixture().Build<GetSummaryInput>()
                .With(x => x.CpfCnpj, 12346)
                .Create();
            var expectedMock = new DefaultFixture().ContratoMock(Guid.Parse("893ff30f-b77e-4450-9f93-17aaa4de3d95"));
            expectedMock.Pagamentos = null;

            var expected = new List<ContratoDto>() {
                expectedMock
            };

            _contractRepositoryMock.Setup(x => x.GetByClient(It.IsAny<long>()))
                .Returns(expected);

            var useCase = CreateInstance();
            var result = await useCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.GetResult().SaldoDevedorConsolidado.Should().Be(30000M);
            result.GetResult().QuantidadeContratos.Should().Be(1);
            result.GetResult().ParcelasAVencer.Should().Be(12);
        }

        [Fact]
        public async Task GetSummaryUseCaseTests_Should_Execute_WhenContract_IsNull()
        {
            var input = new Fixture().Create<GetSummaryInput>();

            _contractRepositoryMock.Setup(x => x.GetByClient(It.IsAny<long>()))
                .Returns(null as IEnumerable<ContratoDto>);

            var useCase = CreateInstance();
            var result = await useCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().HaveCount(1);
            result.Messages.Should().BeNullOrEmpty();
        }


        private GetSummaryUseCase CreateInstance()
        {
            return new GetSummaryUseCase(_loggerMock.Object,
                _contractRepositoryMock.Object);
        }
    }
}
