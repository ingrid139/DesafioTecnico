using Intituicao.Financeira.Application.Features.Clients.GetSummary.Models;
using Intituicao.Financeira.Application.Features.Contracts.GetContract.Models;
using Intituicao.Financeira.Application.Shared.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Intituicao.Financeira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController(UseCaseHandlerBase<GetSummaryInput, GetSummaryOutput> getSummary) : ControllerBase
    {
        private readonly UseCaseHandlerBase<GetSummaryInput, GetSummaryOutput> _getSummary = getSummary;

        /// <summary>
        /// Get Summary By Client
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="204">Success.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Error.</response>
        /// <param name="request">Request for getting a contract</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The contract.</returns>
        [HttpGet("{cpfCnpj}/resumo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClient(long cpfCnpj, CancellationToken cancellationToken)
        {
            var request = new GetSummaryInput() { CpfCnpj = cpfCnpj, CorrelationId = Guid.NewGuid() };
            var output = await _getSummary.ExecuteAsync(request, cancellationToken);

            if (output.IsValid)
                return output.GetResult() is not null ? Ok(output.GetResult()) : NoContent();

            return BadRequest(output);
        }
    }
}
