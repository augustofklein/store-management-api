using StoreManagement.Application.Auth.Handler;
using StoreManagement.Application.Product.Handler;

namespace StoreManagement.WebApi.DependencyInjection
{
    public static class HandlerInjection
    {
        public static IServiceCollection AddHandles(this IServiceCollection services)
        {
            services.AddScoped<AuthHandler>();
            services.AddScoped<ProductHandler>();

            return services;
        }
    }
}
