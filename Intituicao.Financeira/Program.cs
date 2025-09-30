using Intituicao.Financeira.Application.Features.Clients.DependencyInjection;
using Intituicao.Financeira.Application.Features.Contracts.DenpendencyInjection;
using Intituicao.Financeira.Application.Features.Payments.DependencyInjection;
using Intituicao.Financeira.Application.Shared.Repository;
using Intituicao.Financeira.Application.Shared.Repository.Configuration;
using Intituicao.Financeira.DenpendencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Intituicao.Financeira
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGenoptions();
            builder.Services.AddHealthChecks();
            builder.Services.AddDbContext<InstituicaoContexto>();

            //Repositories
            builder.Services.AddRepositories();

            //UseCases
            builder.Services.AddContracts();
            builder.Services.AddPayments();
            builder.Services.AddSummary();
            
            var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Authentication:Secret").Value);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            // swagger
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                app.MapControllers();
            });

            app.Run();
        }
    }
}
