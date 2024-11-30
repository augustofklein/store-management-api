namespace StoreManagement.WebApi.DependencyInjection
{
    public static class MediatorInjection
    {
        public static IServiceCollection AddMediatorInjection(this IServiceCollection services)
        {
            try
            {
                var assembly = AppDomain.CurrentDomain.Load("StoreManagement.Application");
                services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
            }
            catch (FileNotFoundException ex)
            {
                // Log the error or handle it as necessary
                Console.WriteLine($"Error loading assembly: {ex.Message}");
                throw;
            }

            return services;
        }
    }
}
