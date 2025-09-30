using Intituicao.Financeira.Application.Features.Payments.InsertPayment.Models;
using Intituicao.Financeira.Application.Features.Payments.InsertPayment.UseCase;
using Intituicao.Financeira.Application.Shared.Services;
using Intituicao.Financeira.Application.Shared.Core;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Intituicao.Financeira.Application.Shared.Repository.Models;
using Microsoft.Extensions.Logging;
using AutoFixture;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation;
using Mapster;
using Moq;


namespace Instituicao.Financeira.Tests.UseCases
{
    public class InsertPaymentUseCaseTests
    {
        private readonly Mock<ILogger<UseCaseHandlerBase<InsertPaymentInput, InsertPaymentOutput>>> _loggerMock;
        private readonly Mock<IValidator<InsertPaymentInput>> _validatorMock;
        private readonly Mock<IContractRepository> _contractRepositoryMock;
        private readonly Mock<IPaymentRepository> _paymentRepository;
        private readonly Mock<IPriceCalculatorService> _priceCalculatorService;

        public InsertPaymentUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<UseCaseHandlerBase<InsertPaymentInput, InsertPaymentOutput>>>();
            _validatorMock = new Mock<IValidator<InsertPaymentInput>>();
            _contractRepositoryMock = new Mock<IContractRepository>();
            _paymentRepository = new Mock<IPaymentRepository>();
            _priceCalculatorService = new Mock<IPriceCalculatorService>();
        }

        [Fact]
        public async Task CreateContractUseCase_Should_Execute()
        {
            var input = new Fixture().Create<InsertPaymentInput>();
            var expected = input.Adapt<ContratoDto>();
            var expectedContract = new DefaultFixture().ContratoMock(Guid.Parse("b10af031-6b8c-408a-bbbe-509e1b903d4e"));

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<InsertPaymentInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _contractRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                     .Returns(expectedContract);

            _paymentRepository.Setup(x => x.GetByContract(It.IsAny<Guid>()))
                .Returns(new List<PagamentoDto>() { new DefaultFixture().PagamentoMock(Guid.Parse("188720bc-ea13-4966-b7c1-2382a8c6235c"), Guid.Parse("b10af031-6b8c-408a-bbbe-509e1b903d4e")) });

            _priceCalculatorService.Setup(x => x.CalcularSaldoDevedor(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<int>()))
                .Returns(23000M);

            _paymentRepository.Setup(x => x.Insert(It.IsAny<PagamentoDto>()))
               .Returns(new PagamentoDto()
               {
                   DataPagamento = DateTime.Now,
                   ContratoId = Guid.Parse("b10af031-6b8c-408a-bbbe-509e1b903d4e"),
                   DataVencimento = DateTime.Now.AddDays(30),
                   StatusPagamentoId = 1,
                   SaldoDevedor = 23000M
               });

            var useCase = CreateInstance();
            var result = await useCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.GetResult().ContractId.Should().Be(Guid.Parse("b10af031-6b8c-408a-bbbe-509e1b903d4e"));
        }

        [Fact]
        public async Task CreateContractUseCase_Should_Execute_Without_Payments()
        {
            var input = new Fixture().Create<InsertPaymentInput>();
            var expected = input.Adapt<ContratoDto>();
            var expectedContract = new DefaultFixture().ContratoMock(Guid.Parse("b10af031-6b8c-408a-bbbe-509e1b903d4e"));

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<InsertPaymentInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _contractRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                     .Returns(expectedContract);

            _paymentRepository.Setup(x => x.GetByContract(It.IsAny<Guid>()))
                .Returns(new List<PagamentoDto>());

            _priceCalculatorService.Setup(x => x.CalcularSaldoDevedor(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<int>()))
                .Returns(23000M);

            _paymentRepository.Setup(x => x.Insert(It.IsAny<PagamentoDto>()))
               .Returns(new PagamentoDto()
               {
                   DataPagamento = DateTime.Now,
                   ContratoId = Guid.Parse("b10af031-6b8c-408a-bbbe-509e1b903d4e"),
                   DataVencimento = DateTime.Now.AddDays(30),
                   StatusPagamentoId = 1,
                   SaldoDevedor = 23000M
               });

            var useCase = CreateInstance();
            var result = await useCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.GetResult().ContractId.Should().Be(Guid.Parse("b10af031-6b8c-408a-bbbe-509e1b903d4e"));
        }

        [Fact]
        public async Task CreateContractUseCase_Should_Execute_ContractIsNull()
        {
            var input = new Fixture().Create<InsertPaymentInput>();

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<InsertPaymentInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _contractRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                     .Returns(null as ContratoDto);

            var useCase = CreateInstance();
            var result = await useCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().HaveCount(1);
        }

        [Fact]
        public async Task CreateContractUseCase_Should_Validate()
        {
            var input = new Fixture().Create<InsertPaymentInput>();
            input.Invoking(x => x.Id = Guid.Empty);
            var expected = input.Adapt<ContratoDto>();
            var expectedContract = new DefaultFixture().ContratoMock(Guid.Parse("b10af031-6b8c-408a-bbbe-509e1b903d4e"));


            var validationResult = new ValidationResult(
                new List<ValidationFailure> {
                new(nameof(input.Id), "Id is required.")
            });

            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<InsertPaymentInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            _contractRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(expectedContract);

            var useCase = CreateInstance();
            var result = await useCase.ExecuteAsync(input, CancellationToken.None).ConfigureAwait(false);

            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().HaveCount(1);
            result.ErrorMessages.First().Should().Be("Id is required.");
        }

        private InsertPaymentUseCase CreateInstance()
        {
            return new InsertPaymentUseCase(_loggerMock.Object,
                                            _validatorMock.Object,
                                            _contractRepositoryMock.Object,
                                            _paymentRepository.Object,
                                            _priceCalculatorService.Object);
        }
    }
}
