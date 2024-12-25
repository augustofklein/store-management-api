using StoreManagement.Application.Product.Queries;

namespace StoreManagement.WebApi.DependencyInjection
{
    public static class QueriesInjection
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddScoped<IProductQueries, ProductQueries>();

            return services;
        }
    }
}
