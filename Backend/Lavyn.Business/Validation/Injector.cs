using FluentValidation;
using FluentValidation.AspNetCore;
using Lavyn.Business.Validation.Validators;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Lavyn.Business.Validation
{
    public static class Injector
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CredentialDto>, CredentialValidator>();
            services.AddTransient<IValidator<User>, RegisterUserValidator>();

            services.AddTransient<IValidatorInterceptor, ValidationInterceptor>();

            return services;
        }
    }
}