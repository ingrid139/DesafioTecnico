using FluentValidation;
using Intituicao.Financeira.Application.Features.Payments.InsertPayment.Models;

namespace Intituicao.Financeira.Application.Features.Payments.InsertPayment.Validators
{
    public class InsertPaymentInputValidation : AbstractValidator<InsertPaymentInput>
    {
        public InsertPaymentInputValidation()
        {
            RuleFor(x => x.Id)
                  .NotEmpty()
                  .WithMessage("Id is required.");
        }
    }
}
