using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Intituicao.Financeira.Application.Features.Contracts.GetContract.Models;
using Intituicao.Financeira.Application.Features.Contracts.GetContract.UseCase;
using Intituicao.Financeira.Application.Shared.Core;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Intituicao.Financeira.Application.Shared.Repository.Models;

namespace Instituicao.Financeira.Tests.UseCases
{
    public class GetContractUseCaseTests
    {
        private readonly Mock<ILogger<UseCaseHandlerBase<GetContractInput, GetContractOutput>>> _loggerMock;
        private readonly Mock<IContractRepository> _contractRepositoryMock;

        public GetContractUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<UseCaseHandlerBase<GetContractInput, GetContractOutput>>>();
            _contractRepositoryMock = new Mock<IContractRepository>();
        }

        [Fact]
        public async Task GetContractUseCase_Should_Execute()
        {
            var input = new Fixture().Create<GetContractInput>();
            var expected = new DefaultFixture().ContratoMock(Guid.Parse("b10af031-6b8c-408a-bbbe-509e1b903d4e"));

            _contractRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(expected);

            var useCase = CreateInstance();
            var result = await useCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.GetResult().Id.Should().Be(expected.Id);
        }

        [Fact]
        public async Task GetContractUseCase_Should_Execute_WhenContract_IsNull()
        {
            var input = new Fixture().Create<GetContractInput>();

            _contractRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(null as ContratoDto);

            var useCase = CreateInstance();
            var result = await useCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.Messages.Should().BeNullOrEmpty();
        }


        private GetContractUseCase CreateInstance()
        {
            return new GetContractUseCase(_loggerMock.Object,
                _contractRepositoryMock.Object);
        }
    }
}
