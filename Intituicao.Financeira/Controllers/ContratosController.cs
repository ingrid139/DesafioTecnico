using Intituicao.Financeira.Application.Features.Contracts.CreateContract.Models;
using Intituicao.Financeira.Application.Features.Contracts.DeleteContract.Models;
using Intituicao.Financeira.Application.Features.Contracts.GetContract.Models;
using Intituicao.Financeira.Application.Features.Payments.InsertPayment.Models;
using Intituicao.Financeira.Application.Shared.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Intituicao.Financeira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratosController(
        UseCaseHandlerBase<GetContractInput, GetContractOutput> getContract,
        UseCaseHandlerBase<DeleteContractInput, DeleteContractOutput> deleteContract,
        UseCaseHandlerBase<CreateContractInput, CreateContractOutput> createContract,
        UseCaseHandlerBase<InsertPaymentInput, InsertPaymentOutput> createPayment) : ControllerBase
    {
        private readonly UseCaseHandlerBase<GetContractInput, GetContractOutput> _getContract = getContract;
        private readonly UseCaseHandlerBase<DeleteContractInput, DeleteContractOutput> _deleteContract = deleteContract;
        private readonly UseCaseHandlerBase<CreateContractInput, CreateContractOutput> _createContract = createContract;
        private readonly UseCaseHandlerBase<InsertPaymentInput, InsertPaymentOutput> _createPayment = createPayment;

        /// <summary>
        /// Get Contract By Id
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="204">Success.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Error.</response>
        /// <param name="request">Request for getting a contract</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The contract.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetContracts([FromQuery] GetContractInput request, CancellationToken cancellationToken)
        {
            var output = await _getContract.ExecuteAsync(request, cancellationToken);

            if (output.IsValid)
                return output.GetResult() is not null ? Ok(output.GetResult()) : NoContent();

            return BadRequest(output);
        }


        /// <summary>
        /// Create Contract
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Error.</response>
        /// <param name="request">Request for getting a contract</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The contract.</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateContractInput request, CancellationToken cancellationToken)
        {
            var output = await _createContract.ExecuteAsync(request, cancellationToken);

            if (output.IsValid)
                return Ok(output.GetResult());

            return BadRequest(output);
        }

        /// <summary>
        /// Delete Contract By Id
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Error.</response>
        /// <param name="request">Request for delete a contract</param>
        /// <param name="cancellationToken"></param>
        /// <returns>true/false.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] DeleteContractInput request, CancellationToken cancellationToken)
        {
            var output = await _deleteContract.ExecuteAsync(request, cancellationToken);

            if (output.IsValid)
                return Ok(output.GetResult());

            return BadRequest(output);
        }

        /// <summary>
        /// Insert Payment
        /// </summary>
        /// <response code="201">Success.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Error.</response>
        /// <param name="request">Request for inserting a payment by contract</param>
        /// <param name="cancellationToken"></param>
        /// <returns>true/false.</returns>
        [HttpPost("{id}/pagamentos")]
        public async Task<IActionResult> InsertPayments(Guid id, CancellationToken cancellationToken)
        {
            var request = new InsertPaymentInput { Id = id , CorrelationId = Guid.NewGuid() };
            var output = await _createPayment.ExecuteAsync(request, cancellationToken);

            if (output.IsValid)
                return Created();

            return BadRequest(output);
            
        }
    }
}
