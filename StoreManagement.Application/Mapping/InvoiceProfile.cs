using AutoMapper;
using StoreManagement.Application.Invoice.Command;
using StoreManagement.Application.Invoice.Model;

namespace StoreManagement.Application.Mapping
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<AddInvoiceCommand, AddInvoiceDto>();
        }
    }
}
