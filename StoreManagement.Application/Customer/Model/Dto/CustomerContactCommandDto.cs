namespace StoreManagement.Application.Customer.Model.Dto
{
    public class CustomerContactCommandDto
    {
        public int ContactType { get; set; }
        public string ContactDescription { get; set; } = string.Empty;
    }
}
