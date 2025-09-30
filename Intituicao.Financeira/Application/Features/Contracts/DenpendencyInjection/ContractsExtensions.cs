using FluentValidation;
using Intituicao.Financeira.Application.Features.Contracts.CreateContract.Models;
using Intituicao.Financeira.Application.Features.Contracts.CreateContract.UseCase;
using Intituicao.Financeira.Application.Features.Contracts.CreateContract.Validators;
using Intituicao.Financeira.Application.Features.Contracts.DeleteContract.Models;
using Intituicao.Financeira.Application.Features.Contracts.DeleteContract.UseCase;
using Intituicao.Financeira.Application.Features.Contracts.GetContract.Models;
using Intituicao.Financeira.Application.Features.Contracts.GetContract.UseCase;
using Intituicao.Financeira.Application.Shared.Core;
using System.Diagnostics.CodeAnalysis;

namespace Intituicao.Financeira.Application.Features.Contracts.DenpendencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ContractsExtensions
    {
        public static IServiceCollection AddContracts(this IServiceCollection services)
        {
            //Validators
            services.AddSingleton<IValidator<CreateContractInput>, CreateContractValidation>();

            //Usecases
            services.AddScoped<UseCaseHandlerBase<GetContractInput, GetContractOutput>, GetContractUseCase>();
            services.AddScoped<UseCaseHandlerBase<DeleteContractInput, DeleteContractOutput>, DeleteContractUseCase>();
            services.AddScoped<UseCaseHandlerBase<CreateContractInput, CreateContractOutput>, CreateContractUseCase>();

            return services;
        }
    }
}
