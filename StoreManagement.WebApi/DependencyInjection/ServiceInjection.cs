using StoreManagement.Application.Auth.Service;
using StoreManagement.Application.Product.Service;

namespace StoreManagement.WebApi.DependencyInjection
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
