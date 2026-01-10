using CSharpFunctionalExtensions;
using MediatR;
using StoreManagement.Application.Customer.Model.Dto;

namespace StoreManagement.Application.Customer.Command
{
    public class AddCustomerCommand(string documentNumber, string name, string address, List<CustomerContactCommandDto> customerContacts) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public string DocumentNumber { get; set; } = documentNumber;
        public string Name { get; set; } = name;
        public string Address { get; set; } = address;
        public List<CustomerContactCommandDto> CustomerContacts { get; set; } = customerContacts;
    }
}