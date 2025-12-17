using CSharpFunctionalExtensions;
using MediatR;
using static StoreManagement.Application.Customer.Command.AddCustomerCommand;

namespace StoreManagement.Application.Customer.Command
{
    public class AddCustomerCommand(string identification, string address, List<CustomerContact> customerContacts) : IRequest<Result>
    {
        public int CompanyId { get; set; }
        public string Identification { get; set; } = identification;
        public string Address { get; set; } = address;
        public List<CustomerContact> CustomerContacts { get; set; } = customerContacts;

        public class CustomerContact
        {
            public int ContactType { get; set; }
            public string ContactDescription { get; set; } = string.Empty;
        }
    }
}
