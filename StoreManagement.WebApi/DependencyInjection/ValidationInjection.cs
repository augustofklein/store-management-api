using FluentValidation;

namespace StoreManagement.WebApi.DependencyInjection;

public static class ValidationInjection
{
    public static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.Load("StoreManagement.Application"));

        return services;
    }
}