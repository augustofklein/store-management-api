using System;
using StoreManagement.Application.Auth.Service;

namespace StoreManagement.WebApi.DependencyInjection
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
