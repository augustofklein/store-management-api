using AutoMapper;
using StoreManagement.Application.Product.Model;
using StoreManagement.Infrastructure.DBContext.Model;

namespace StoreManagement.Application.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<ProductEntity, ProductDto>()
                .ForMember(dest => dest.SkuId,
                opt => opt.MapFrom(src => src.SkuId.Trim()));
        }
    }
}
