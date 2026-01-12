namespace StoreManagement.Application.Auth.Model.Dto
{
    public class UserCompaniesDto
    {
        public int CompanyId { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
    }
}
