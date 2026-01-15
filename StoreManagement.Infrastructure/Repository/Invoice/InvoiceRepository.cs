using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Invoice.Model;
using StoreManagement.Domain.Entities;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Infrastructure.Repository.Invoice
{
    public class InvoiceRepository(AppDbContext dbContext) : IInvoiceRepository
    {
        public async Task<Result<IEnumerable<InvoiceDto>>> ReturnAllInvoicesAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await dbContext.Invoice
                .Include(i => i.InvoiceItems)
                    .ThenInclude(ii => ii.Product)
                .Where(i => i.CompanyId == companyId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(i => new InvoiceDto
                {
                    InvoiceId = i.Id,
                    InvoiceDate = i.InvoiceDate,
                    TotalAmount = i.TotalAmount,
                    Customer = new InvoiceDto.CustomerInvoice
                    {
                        Id = i.Customer.Id,
                        DocumentNumber = i.Customer.DocumentNumber,
                        Name = i.Customer.Name,
                        Address = i.Customer.Address
                    },
                    Items = i.InvoiceItems.Select(ii => new InvoiceDto.InvoiceItem
                    {
                        ProductId = ii.ProductId,
                        SkuId = ii.Product.SkuId,
                        Barcode = ii.Product.Barcode,
                        Description = ii.Product.Description,
                        Price = ii.Price,
                        Quantity = ii.Quantity
                    }).ToList()
                }).ToListAsync(cancellationToken);
        }

        public async Task AddInvoiceAsync(AddInvoiceDto invoice, CancellationToken cancellationToken)
        {
            var invoiceEntity = new InvoiceEntity
            {
                CompanyId = invoice.CompanyId,
                CustomerId = invoice.CustomerId,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.InvoiceItems.Sum(p =>
                {
                    return p.Price * p.Quantity;
                }),
                InvoiceItems = [.. invoice.InvoiceItems.Select(ii => new InvoiceItemEntity
                {
                    ProductId = ii.ProductId,
                    Price = ii.Price,
                    Quantity = ii.Quantity
                })]
            };

            await dbContext.Invoice.AddAsync(invoiceEntity, cancellationToken);
        }
    }
}
