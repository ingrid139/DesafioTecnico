using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Intituicao.Financeira.Application.Features.Contracts.CreateContract.Models;
using Intituicao.Financeira.Application.Features.Contracts.CreateContract.UseCase;
using Intituicao.Financeira.Application.Shared.Core;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Intituicao.Financeira.Application.Shared.Repository.Models;
using Mapster;
using Microsoft.Extensions.Logging;
using Moq;


namespace Instituicao.Financeira.Tests.UseCases
{
    public class CreateContractUseCaseTests
    {
        private readonly Mock<ILogger<UseCaseHandlerBase<CreateContractInput, CreateContractOutput>>> _loggerMock;
        private readonly Mock<IValidator<CreateContractInput>> _validatorMock;
        private readonly Mock<IContractRepository> _contractRepositoryMock;

        public CreateContractUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<UseCaseHandlerBase<CreateContractInput, CreateContractOutput>>>();
            _validatorMock = new Mock<IValidator<CreateContractInput>>();
            _contractRepositoryMock = new Mock<IContractRepository>();
        }

        [Fact]
        public async Task CreateContractUseCase_Should_Execute()
        {
            var input = new Fixture().Create<CreateContractInput>();
            var expected = input.Adapt<ContratoDto>();

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CreateContractInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _contractRepositoryMock.Setup(x => x.Insert(It.IsAny<ContratoDto>()))
                .Returns(expected);

            var useCase = CreateInstance();
            var result = await useCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.GetResult().Id.Should().Be(expected.Id);
        }

        [Fact]
        public async Task CreateContractUseCase_Should_Validate()
        {
            var input = new Fixture().Create<CreateContractInput>();
               input.Invoking(x => x.Id = Guid.Empty);
            var expected = input.Adapt<ContratoDto>();


            var validationResult = new ValidationResult(
                new List<ValidationFailure> { 
                new(nameof(input.Id), "Id is required.")
            });

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CreateContractInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            _contractRepositoryMock.Setup(x => x.Insert(It.IsAny<ContratoDto>()))
                .Returns(expected);

            var useCase = CreateInstance();
            var result = await useCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().HaveCount(1);
            result.ErrorMessages.First().Should().Be("Id is required.");
        }

        private CreateContractUseCase CreateInstance()
        {
            return new CreateContractUseCase(_loggerMock.Object,
                _validatorMock.Object,
                _contractRepositoryMock.Object);
        }
    }
}
