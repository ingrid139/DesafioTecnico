using FluentValidation;
using Intituicao.Financeira.Application.Features.Payments.InsertPayment.Models;
using Intituicao.Financeira.Application.Features.Payments.InsertPayment.UseCase;
using Intituicao.Financeira.Application.Features.Payments.InsertPayment.Validators;
using Intituicao.Financeira.Application.Shared.Core;
using Intituicao.Financeira.Application.Shared.Services;
using System.Diagnostics.CodeAnalysis;

namespace Intituicao.Financeira.Application.Features.Payments.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class PaymentsExtensions
    {
        public static IServiceCollection AddPayments(this IServiceCollection services)
        {
            //Validators
            services.AddSingleton<IValidator<InsertPaymentInput>, InsertPaymentInputValidation>();

            //Usecases
            services.AddScoped<UseCaseHandlerBase<InsertPaymentInput, InsertPaymentOutput>, InsertPaymentUseCase>();

            //Services
            services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();

            return services;
        }
    }
}
