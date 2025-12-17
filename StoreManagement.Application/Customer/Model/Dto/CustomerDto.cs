namespace StoreManagement.Application.Customer.Model.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public List<CustomerContactDto> CustomerContacts { get; set; } = [];
    }
}
