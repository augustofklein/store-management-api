using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Purchase.Model;
using StoreManagement.Common;

namespace StoreManagement.Application.Purchase.Command
{
    public class AddPurchaseCommand(AddPurchaseDocumentDto document, List<AddPurchaseItemDto> products) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public int SupplierId { get; set; }
        public AddPurchaseDocumentDto Document { get; set; } = document;
        public List<AddPurchaseItemDto> Products { get; set; } = products;
    }
}