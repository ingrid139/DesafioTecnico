using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;


namespace Intituicao.Financeira.DenpendencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerGenoptions(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API - Instituição Financeira",
                    Description = "Desafio Técnico - Instituição Financeira",
                    Contact = new OpenApiContact
                    {
                        Name = "Ingrid Oliveira",
                        Email = "ingrid139@hotmail.com"
                    }
                });
                
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                   {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "apiKey",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
