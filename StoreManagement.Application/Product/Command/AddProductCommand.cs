using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class AddProductCommand(string skuId, bool status, string barcode, string description, int stock, decimal price) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public string SkuId { get; private set; } = skuId;
        public bool Status { get; set; } = status;
        public string Barcode { get; private set; } = barcode;
        public string Description { get; private set; } = description;
        public int Stock { get; private set; } = stock;
        public decimal Price { get; set; } = price;
    }
}
