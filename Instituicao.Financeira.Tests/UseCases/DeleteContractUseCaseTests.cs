using Intituicao.Financeira.Application.Features.Contracts.DeleteContract.Models;
using Intituicao.Financeira.Application.Features.Contracts.DeleteContract.UseCase;
using Intituicao.Financeira.Application.Shared.Core;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Instituicao.Financeira.Tests.UseCases
{
    public class DeleteContractUseCaseTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<ILogger<UseCaseHandlerBase<DeleteContractInput, DeleteContractOutput>>> _loggerMock;
        private readonly Mock<IContractRepository> _contractRepositoryMock;
        private readonly DeleteContractUseCase _deleteContractUseCase;

        public DeleteContractUseCaseTests()
        {
            _fixture = new Fixture();
            _loggerMock = new Mock<ILogger<UseCaseHandlerBase<DeleteContractInput, DeleteContractOutput>>>();
            _contractRepositoryMock = new Mock<IContractRepository>();
            _deleteContractUseCase = new DeleteContractUseCase(_loggerMock.Object, _contractRepositoryMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_Should_DeleteContract_WhenContractExists()
        {
            // Arrange
            var input = _fixture.Create<DeleteContractInput>();

            _contractRepositoryMock.Setup(repo => repo.DeleteById(It.IsAny<Guid>()))
                .Returns(true);

            // Act
            var result = await _deleteContractUseCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.GetResult().IsDeleted.Should().BeTrue();
            _contractRepositoryMock.Verify(repo => repo.DeleteById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_Should_ThrowException_WhenContractDoesNotExist()
        {
            // Arrange
            var input = _fixture.Create<DeleteContractInput>();

            _contractRepositoryMock.Setup(repo => repo.DeleteById(It.IsAny<Guid>()))
                .Returns(false);

            // Act
            var result = await _deleteContractUseCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            // Assert
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.GetResult().IsDeleted.Should().BeFalse();
            _contractRepositoryMock.Verify(repo => repo.DeleteById(It.IsAny<Guid>()), Times.Once);
        }

    }
}