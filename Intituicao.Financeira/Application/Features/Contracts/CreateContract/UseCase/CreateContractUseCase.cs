using FluentValidation;
using Intituicao.Financeira.Application.Features.Contracts.CreateContract.Models;
using Intituicao.Financeira.Application.Shared.Core;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Intituicao.Financeira.Application.Shared.Repository.Models;
using Mapster;

namespace Intituicao.Financeira.Application.Features.Contracts.CreateContract.UseCase
{
    public class CreateContractUseCase(ILogger<UseCaseHandlerBase<CreateContractInput, CreateContractOutput>> logger,
        IValidator<CreateContractInput> validator,
        IContractRepository contratoRepository) : UseCaseHandlerBase<CreateContractInput, CreateContractOutput>(logger, validator)
    {
        private readonly IContractRepository _contratoRepository = contratoRepository;
        private readonly ILogger<UseCaseHandlerBase<CreateContractInput, CreateContractOutput>> _logger = logger;
        protected override async Task<Output<CreateContractOutput>> HandleAsync(CreateContractInput input, CancellationToken cancellationToken)
        {
            var output = new Output<CreateContractOutput>();
            try
            {
                var result = _contratoRepository.Insert(input.Adapt<ContratoDto>());
                output.AddResult(result.Adapt<CreateContractOutput>());
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Contract {contractId} is not created. {CorrelationId} | {error}", input.Id, input.CorrelationId, ex.Message);
                output.AddErrorMessage("Contract is not created.");
            }

            return output;
        }
    }
}
