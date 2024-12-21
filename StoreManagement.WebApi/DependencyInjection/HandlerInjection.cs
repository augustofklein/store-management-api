using StoreManagement.Application.Auth.Handler;

namespace StoreManagement.WebApi.DependencyInjection
{
    public static class HandlerInjection
    {
        public static IServiceCollection AddHandles(this IServiceCollection services)
        {
            services.AddScoped<AuthHandler>();

            return services;
        }
    }
}
