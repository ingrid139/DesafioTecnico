using Intituicao.Financeira.Application.Features.Clients.GetSummary.Models;
using Intituicao.Financeira.Application.Features.Clients.GetSummary.UseCase;
using Intituicao.Financeira.Application.Shared.Core;
using System.Diagnostics.CodeAnalysis;

namespace Intituicao.Financeira.Application.Features.Clients.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class GetSummaryExtensions
    {
        public static IServiceCollection AddSummary(this IServiceCollection services)
        {
            //Usecases
            services.AddScoped<UseCaseHandlerBase<GetSummaryInput, GetSummaryOutput>, GetSummaryUseCase>();

            return services;
        }
    }
}
