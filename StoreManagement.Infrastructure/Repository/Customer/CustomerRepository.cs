using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Customer.Model.Dto;
using StoreManagement.Infrastructure.DBContext;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Infrastructure.Repository.Customer
{
    public class CustomerRepository(AppDbContext dbContext) : ICustomerRepository
    {
        public async Task<bool> VerifyCustomerByIdentificationExistAsync(int companyId, string identification, CancellationToken cancellationToken)
        {
            return await dbContext.Customers
                .Where(c => c.CompanyId == companyId && c.Identification == identification)
                .FirstOrDefaultAsync(cancellationToken) != null;
        }

        public async Task<Result<IEnumerable<CustomerDto>>> ReturnAllCustomersAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await dbContext.Customers
                .AsNoTracking()
                .Include(c => c.CustomerContacts)
                    .ThenInclude(cc => cc.ContactType)
                .Where(c => c.CompanyId == companyId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Identification = c.Identification,
                    Address = c.Address,
                    CustomerContacts = c.CustomerContacts.Select(ct => new CustomerContactDto
                    {
                        ContactId = ct.Id,
                        Description = ct.ContactType.Description,
                        ContactType = ct.ContactType.Id,
                        Contact = ct.Contact
                    })
                    .ToList(),
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> AddCustomerAsync(int companyId, CustomerDto customer, CancellationToken cancellationToken)
        {
            var customerEntity = new CustomerEntity
            {
                CompanyId = companyId,
                Identification = customer.Identification,
                Address = customer.Address,

                CustomerContacts = customer.CustomerContacts.Select(c =>
                    new CustomerContactEntity
                    {
                        CustomerId = 0,
                        ContactTypeId = c.ContactType,
                        Contact = c.Contact
                    }).ToList()
            };

            await dbContext.Customers.AddAsync(customerEntity, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            //var customerId = await dbContext.Customers
            //    .Where(c => c.Identification == customer.Identification)
            //    .Select(c => c.Id)
            //    .FirstAsync(cancellationToken);

            //foreach (CustomerContactDto contactItem in customer.CustomerContacts)
            //{
            //    var contactRegister = new CustomerContactEntity
            //    {
            //        CustomerId = customerId,
            //        ContactId = contactItem.ContactId,
            //        Id = 0,
            //        Contact = contactItem.Contact
            //    };

            //    await dbContext.CustomerContacts.AddAsync(contactRegister, cancellationToken);
            //}

            return true;
        }
    }
}
