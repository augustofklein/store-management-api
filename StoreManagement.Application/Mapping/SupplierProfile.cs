using AutoMapper;
using StoreManagement.Application.Supplier.Command;
using StoreManagement.Application.Supplier.Model;

namespace StoreManagement.Application.Mapping
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<AddSupplierCommand, AddSupplierDto>();
        }
    }
}
