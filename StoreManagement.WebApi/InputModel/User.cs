namespace StoreManagement.WebApi.InputModel
{
    public class User(string username, string password, int companyId)
    {
        public string Username { get; private set; } = username;
        public string Password { get; private set; } = password;
        public int CompanyId { get; set; } = companyId;
    }
}
