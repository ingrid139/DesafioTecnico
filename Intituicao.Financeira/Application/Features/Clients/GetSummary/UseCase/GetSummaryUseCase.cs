using Intituicao.Financeira.Application.Features.Clients.GetSummary.Models;
using Intituicao.Financeira.Application.Shared.Core;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Intituicao.Financeira.Application.Shared.Repository.Models;

namespace Intituicao.Financeira.Application.Features.Clients.GetSummary.UseCase
{
    public class GetSummaryUseCase(ILogger<UseCaseHandlerBase<GetSummaryInput, GetSummaryOutput>> logger,
        IContractRepository contractRepository) : UseCaseHandlerBase<GetSummaryInput, GetSummaryOutput>(logger)
    {
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly ILogger<UseCaseHandlerBase<GetSummaryInput, GetSummaryOutput>> _logger = logger;
        protected override async Task<Output<GetSummaryOutput>> HandleAsync(GetSummaryInput input, CancellationToken cancellationToken)
        {
            var output = new Output<GetSummaryOutput>();

            var contratos = _contractRepository.GetByClient(input.CpfCnpj);
            if (contratos is null)
            {
                _logger.LogWarning("Summary | Contracts by {client} not found. {CorrelationId}", input.CpfCnpj, input.CorrelationId);
                output.AddErrorMessage("Contracts by client not found.");
                return output;
            }

            output.AddResult(GetSummary(contratos));
            return output;
        }

        private GetSummaryOutput GetSummary(IEnumerable<ContratoDto> contratos)
        {
            int quantidade = contratos.Count();
            int emDia = 0;
            int emAtraso = 0;
            int aVencer = 0;
            int totalParcelas = 0;
            decimal saldo = 0M;

            foreach (var contrato in contratos)
            {
                if (contrato.Pagamentos is not null && contrato.Pagamentos.Any())
                {
                    emDia += contrato.Pagamentos.Where(x => x.StatusPagamentoId == 1).Count();
                    emAtraso += contrato.Pagamentos.Where(x => x.StatusPagamentoId == 2).Count();
                    saldo += contrato.Pagamentos.LastOrDefault().SaldoDevedor;
                }
                else
                {
                    saldo += contrato.ValorTotal;
                }
                aVencer += (contrato.PrazoMeses - (emDia + emAtraso));
                totalParcelas += contrato.PrazoMeses;
            }

            var percentual = (decimal)emDia / totalParcelas * 100m;

            return new GetSummaryOutput(quantidade, emDia, emAtraso, aVencer, Math.Round(percentual, 2), saldo);
        }
    }
}
