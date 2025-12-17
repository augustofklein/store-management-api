using StoreManagement.Application.Auth.Service;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Customer.Service;
using StoreManagement.Application.Product.Service;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.WebApi.DependencyInjection
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IEFTransactionManager, EfTransactionManager>();

            return services;
        }
    }
}
