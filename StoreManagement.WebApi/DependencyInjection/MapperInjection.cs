namespace StoreManagement.WebApi.DependencyInjection
{
    public static class MapperInjection
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.Load("StoreManagement.Application"));

            return services;
        }
    }
}
