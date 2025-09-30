using Intituicao.Financeira.Application.Features.Contracts.DeleteContract.Models;
using Intituicao.Financeira.Application.Shared.Core;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Mapster;

namespace Intituicao.Financeira.Application.Features.Contracts.DeleteContract.UseCase
{
    public class DeleteContractUseCase(ILogger<UseCaseHandlerBase<DeleteContractInput, DeleteContractOutput>> logger,
        IContractRepository contratoRepository) : UseCaseHandlerBase<DeleteContractInput, DeleteContractOutput>(logger)
    {
        private readonly IContractRepository _contratoRepository = contratoRepository;
        private readonly ILogger<UseCaseHandlerBase<DeleteContractInput, DeleteContractOutput>> _logger = logger;
        protected override async Task<Output<DeleteContractOutput>> HandleAsync(DeleteContractInput input, CancellationToken cancellationToken)
        {
            var output = new Output<DeleteContractOutput>();

            try
            {
                var contrato = _contratoRepository.DeleteById(input.Id);
                output.AddResult(new DeleteContractOutput() { IsDeleted = contrato });
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Contract {contractId} not deleted. {CorrelationId} | {error}", input.Id, input.CorrelationId, ex.Message);
                output.AddErrorMessage("Contract not deleted.");
            }
            
            return output;
        }
    }
}
