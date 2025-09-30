using Intituicao.Financeira.Application.Features.Contracts.GetContract.Models;
using Intituicao.Financeira.Application.Shared.Core;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Mapster;

namespace Intituicao.Financeira.Application.Features.Contracts.GetContract.UseCase
{
    public class GetContractUseCase(ILogger<UseCaseHandlerBase<GetContractInput, GetContractOutput>> logger,
        IContractRepository contractRepository) : UseCaseHandlerBase<GetContractInput, GetContractOutput>(logger)
    {
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly ILogger<UseCaseHandlerBase<GetContractInput, GetContractOutput>> _logger = logger;
        protected override async Task<Output<GetContractOutput>> HandleAsync(GetContractInput input, CancellationToken cancellationToken)
        {
            var output = new Output<GetContractOutput>();

            var contrato = _contractRepository.GetById(input.Id);
            if (contrato is null)
            {
                _logger.LogWarning("Contract {contractId} not found. {CorrelationId}", input.Id, input.CorrelationId);
                return output;
            }

            output.AddResult(contrato.Adapt<GetContractOutput>());
            return output;
        }
    }
}
