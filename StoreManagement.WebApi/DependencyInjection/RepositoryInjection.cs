using StoreManagement.Infrastructure.Repository.Product;

namespace StoreManagement.WebApi.DependencyInjection
{
    public static class RepositoryInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
