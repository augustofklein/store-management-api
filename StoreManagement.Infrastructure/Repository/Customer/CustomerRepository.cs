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
        public async Task<bool> VerifyCustomerByDocumentNumberExistAsync(int companyId, string documentNumber, CancellationToken cancellationToken)
        {
            return await dbContext.Customers
                .Where(c => c.CompanyId == companyId && c.DocumentNumber == documentNumber)
                .FirstOrDefaultAsync(cancellationToken) != null;
        }

        public async Task<bool> VerifyCustomerByIdExistAsync(int companyId, int id, CancellationToken cancellationToken)
        {
            return await dbContext.Customers
                .Where(c => c.CompanyId == companyId && c.Id == id)
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
                    DocumentNumber = c.DocumentNumber,
                    Name = c.Name,
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
                DocumentNumber = customer.DocumentNumber,
                Name = customer.Name,
                Address = customer.Address,

                CustomerContacts = [.. customer.CustomerContacts.Select(c =>
                    new CustomerContactEntity
                    {
                        ContactTypeId = c.ContactType,
                        Contact = c.Contact
                    })]
            };

            await dbContext.Customers.AddAsync(customerEntity, cancellationToken);
            var result = await dbContext.SaveChangesAsync(cancellationToken);

            return result > 0;
        }

        public async Task<bool> UpdateCustomerAsync(int companyId, CustomerDto customer, CancellationToken cancellationToken)
        {
            var customerEntity = await dbContext.Customers
                .Include(c => c.CustomerContacts)
                .Where(c => c.CompanyId == companyId && c.Id == customer.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (customerEntity == null)
                return false;

            customerEntity.Name = customer.Name;
            customerEntity.Address = customer.Address;
            
            dbContext.CustomerContacts.RemoveRange(customerEntity.CustomerContacts);
            customerEntity.CustomerContacts = [.. customer.CustomerContacts.Select(c =>
                new CustomerContactEntity
                {
                    ContactTypeId = c.ContactType,
                    Contact = c.Contact
                })];
            
            await dbContext.SaveChangesAsync(cancellationToken);
            
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int companyId, int customerId, CancellationToken cancellationToken)
        {
            var customerEntity = await dbContext.Customers
                .Where(c => c.CompanyId == companyId && c.Id == customerId)
                .FirstOrDefaultAsync(cancellationToken);

            if (customerEntity == null)
                return false;

            dbContext.Customers.Remove(customerEntity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
