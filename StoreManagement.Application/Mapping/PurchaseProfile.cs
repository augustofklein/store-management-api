using AutoMapper;
using StoreManagement.Application.Product.Model;
using StoreManagement.Application.Purchase.Command;
using StoreManagement.Application.Purchase.Model;

namespace StoreManagement.Application.Mapping
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<AddPurchaseCommand, AddPurchaseDto>();
            CreateMap<AddPurchaseItemDto, UpdateProductStockDto>();
            CreateMap<AddPurchaseItemDto, AddProductMovementDto>();
        }
    }
}
