using System.Diagnostics.CodeAnalysis;
using Intituicao.Financeira.Application.Shared.Repository.Interfaces;

namespace Intituicao.Financeira.Application.Shared.Repository.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class PostgreDBExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            return services;
        }

    }
}
