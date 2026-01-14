using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Purchase.Model;

namespace StoreManagement.Application.Purchase.Command
{
    public class AddPurchaseCommand(AddPurchaseDocumentDto document, List<AddPurchaseItemDto> products) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public DateTimeOffset PurchaseEntryDate { get; set; } = DateTimeOffset.Now.DateTime;
        public int SupplierId { get; set; }
        public AddPurchaseDocumentDto Document { get; set; } = document;
        public List<AddPurchaseItemDto> Products { get; set; } = products;
    }
}