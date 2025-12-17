namespace StoreManagement.Application.Customer.Model.Dto
{
    public class CustomerContactDto
    {
        public int ContactId { get; set; }
        public int ContactType { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
    }
}
