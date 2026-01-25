using AutoMapper;
using StoreManagement.Application.Invoice.Model;
using StoreManagement.Application.Product.Model;

namespace StoreManagement.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddInvoiceItemsDto, AddProductMovementDto>();
            CreateMap<AddInvoiceItemsDto, ProductStockDto>();
        }
    }
}
