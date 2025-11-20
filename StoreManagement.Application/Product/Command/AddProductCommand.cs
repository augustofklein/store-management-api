using CSharpFunctionalExtensions;
using MediatR;

namespace StoreManagement.Application.Product.Command
{
    public class AddProductCommand(int companyId, string skuId, bool status, string barcode, string description, int stock) : IRequest<Result>
    {
        public int CompanyId { get; set; } = companyId;
        public string SkuId { get; private set; } = skuId;
        public bool Status { get; set; } = status;
        public string Barcode { get; private set; } = barcode;
        public string Description { get; private set; } = description;
        public int Stock { get; private set; } = stock;

        public static AddProductCommand CreateCommand(int companyId, string skuId, bool status, string barcode, string description, int stock)
            => new(companyId, skuId, status, barcode, description, stock);
    }
}
