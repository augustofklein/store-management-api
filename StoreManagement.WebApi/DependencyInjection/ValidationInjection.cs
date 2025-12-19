using FluentValidation;
using StoreManagement.Application.Validations.Product;

namespace StoreManagement.WebApi.DependencyInjection;

public static class ValidationInjection
{
    public static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddProductCommandValidator>();

        return services;
    }
}