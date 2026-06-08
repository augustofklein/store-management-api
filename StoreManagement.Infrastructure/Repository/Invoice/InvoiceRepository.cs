using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contracts.Persistence;
using StoreManagement.Application.Invoice.Model;
using StoreManagement.Common;
using StoreManagement.Domain.Entities;
using StoreManagement.Infrastructure.DBContext;

namespace StoreManagement.Infrastructure.Repository.Invoice
{
    public class InvoiceRepository(AppDbContext dbContext) : IInvoiceRepository
    {
        public async Task<Result<IEnumerable<InvoiceDto>>> ReturnAllInvoicesAsync(int companyId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var invoices = await dbContext.Invoice
                    .Include(i => i.InvoiceItems)
                        .ThenInclude(ii => ii.Product)
                    .Where(i => i.CompanyId == companyId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(i => new InvoiceDto
                    {
                        InvoiceId = i.Id,
                        InvoiceDate = DateTimeUtils.ToBrazilTime(i.InvoiceDate),
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
                        }).ToList(),
                        Payments = i.InvoicePayments.Select(p => new InvoiceDto.InvoicePayment
                        {
                            PaymentTypeId = p.PaymentTypeId,
                            Amount = p.Amount,
                            PaymentDate = DateTimeUtils.ToBrazilTime(p.PaymentDate)
                        }).ToList()
                    }).ToListAsync(cancellationToken);

                return Result.Success<IEnumerable<InvoiceDto>>(invoices);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<InvoiceDto>>($"An error occurred while retrieving invoices: {ex.Message}");
            }
        }

        public async Task AddInvoiceAsync(AddInvoiceDto invoice, CancellationToken cancellationToken)
        {
            var invoiceEntity = new InvoiceEntity
            {
                CompanyId = invoice.CompanyId,
                CustomerId = invoice.CustomerId,
                InvoiceDate = DateTimeOffset.UtcNow,
                TotalAmount = invoice.InvoiceItems.Sum(p =>
                {
                    return p.Price * p.Quantity;
                }),
                InvoiceItems = [.. invoice.InvoiceItems.Select(ii => new InvoiceItemEntity
                {
                    ProductId = ii.ProductId,
                    Price = ii.Price,
                    Quantity = ii.Quantity
                })],
                InvoicePayments = [.. invoice.InvoicePayments.Select(p => new IncoicePaymentsEntity
                {
                    PaymentTypeId = p.PaymentTypeId,
                    Amount = p.Amount
                })]
            };

            await dbContext.Invoice.AddAsync(invoiceEntity, cancellationToken);
        }

        public async Task<bool> VerifyPaymentsIdExistAsync(int companyId, IEnumerable<int> paymentsId, CancellationToken cancellationToken)
        {
            var existingPaymentsCount = await dbContext.PaymentTypes
                .Where(p => p.CompanyId == companyId && paymentsId.Contains(p.Id))
                .CountAsync(cancellationToken);

            return existingPaymentsCount == paymentsId.Count();
        }
    }
}
