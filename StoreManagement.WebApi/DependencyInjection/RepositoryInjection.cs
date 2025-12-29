using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Infrastructure.Repository.Customer;
using StoreManagement.Infrastructure.Repository.Invoice;
using StoreManagement.Infrastructure.Repository.Product;
using StoreManagement.Infrastructure.Repository.Purchase;
using StoreManagement.Infrastructure.Repository.Supplier;

namespace StoreManagement.WebApi.DependencyInjection
{
    public static class RepositoryInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            return services;
        }
    }
}
