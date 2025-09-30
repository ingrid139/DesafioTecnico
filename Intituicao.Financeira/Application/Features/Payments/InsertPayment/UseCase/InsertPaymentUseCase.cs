using FluentValidation;
using Intituicao.Financeira.Application.Features.Payments.InsertPayment.Models;
using Intituicao.Financeira.Application.Shared.Core;
using Intituicao.Financeira.Application.Shared.Domain.Entities;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Intituicao.Financeira.Application.Shared.Repository.Models;
using Intituicao.Financeira.Application.Shared.Services;
using Mapster;

namespace Intituicao.Financeira.Application.Features.Payments.InsertPayment.UseCase
{
    public class InsertPaymentUseCase(ILogger<UseCaseHandlerBase<InsertPaymentInput, InsertPaymentOutput>> logger,
         IValidator<InsertPaymentInput> validator,
        IContractRepository contractRepository,
        IPaymentRepository paymentRepository,
        IPriceCalculatorService priceCalculatorService) : UseCaseHandlerBase<InsertPaymentInput, InsertPaymentOutput>(logger, validator)
    {
        private readonly ILogger<UseCaseHandlerBase<InsertPaymentInput, InsertPaymentOutput>> _logger = logger;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IPaymentRepository _paymentRepository = paymentRepository;
        private readonly IPriceCalculatorService _priceCalculatorService = priceCalculatorService;
        protected override async Task<Output<InsertPaymentOutput>> HandleAsync(InsertPaymentInput input, CancellationToken cancellationToken)
        {
            var output = new Output<InsertPaymentOutput>();

            try
            {
                var contrato = _contractRepository.GetById(input.Id);

                if (contrato is null)
                {
                    _logger.LogWarning("Contract | Payment {contractId} not found. {CorrelationId}", input.Id, input.CorrelationId);
                    output.AddErrorMessage("Contract | Payment not found.");
                    return output;
                }

                var pagamento = BuildPayment(contrato);

                var result = _paymentRepository.Insert(pagamento.Adapt<PagamentoDto>());
                output.AddResult(new InsertPaymentOutput() { PaymentId = result.Id, ContractId = result.ContratoId });
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Contract | Payment {contractId} not inserted. {CorrelationId} | {error}", input.Id, input.CorrelationId, ex.Message);
                output.AddErrorMessage("Payment not inserted.");
            }

            return output;
        }

        private Pagamento BuildPayment(ContratoDto contrato) 
        {
            var pagamentos = _paymentRepository.GetByContract(contrato.Id);
            var pagamento = new Pagamento(Guid.NewGuid(), contrato.Id);

            if (pagamentos is not null && pagamentos.Any())
            {
                pagamento.SetDataVencimento(pagamentos.LastOrDefault().DataVencimento);
                pagamento.SetDataStatusPagemento();
                pagamento.SetSaldoDevedor(_priceCalculatorService.CalcularSaldoDevedor(pagamentos.LastOrDefault().SaldoDevedor , contrato.TaxaMensal, contrato.PrazoMeses - pagamentos.Count()));
            }
            else
            {
                pagamento.SetPrimeiraDataVencimento(contrato.DataVencimentoPrimeiraParcela);
                pagamento.SetDataStatusPagemento();
                pagamento.SetSaldoDevedor(_priceCalculatorService.CalcularSaldoDevedor(contrato.ValorTotal, contrato.TaxaMensal, contrato.PrazoMeses));
            }
            
            return pagamento;
        }

    }
}
