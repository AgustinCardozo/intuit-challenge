using Cliente.Application.UseCases;
using Cliente.Application.UseCases.Contracts;
using Cliente.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Cliente.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IClienteUseCase, ClienteUseCase>();
            services.AddValidatorsFromAssemblyContaining<InsertClientValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateClientValidator>();
            return services;
        }
    }
}
