using FluentValidation;
using Intituicao.Financeira.Application.Features.Contracts.CreateContract.Models;

namespace Intituicao.Financeira.Application.Features.Contracts.CreateContract.Validators
{
    public class CreateContractValidation : AbstractValidator<CreateContractInput>
    {
        public CreateContractValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
            
            RuleFor(x => x.ClienteCpfCnpj)
                    .NotEmpty()
                    .WithMessage("Cliente Cpf/Cnpj is required.");

            RuleFor(x => x.PrazoMeses)
                    .NotEmpty()
                    .WithMessage("Prazo Meses is required.");

            RuleFor(x => x.TaxaMensal)
                    .NotEmpty()
                    .WithMessage("Taxa Mensal is required.");

            RuleFor(x => x.ValorTotal)
                    .NotEmpty()
                    .WithMessage("Valor Total is required.");

            RuleFor(x => x.DataVencimentoPrimeiraParcela)
                    .NotEmpty()
                    .WithMessage("Data Vencimento Primeira Parcela is required.");

            RuleFor(x => x.CondicaoVeiculoId)
                    .NotEmpty()
                    .WithMessage("Condicao Veiculo is required.");

            RuleFor(x => x.TipoVeiculoId)
                    .NotEmpty()
                    .WithMessage("Tipo Veiculo is required.");
        }
    }
}
