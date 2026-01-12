namespace StoreManagement.WebApi.InputModel.User
{
    public class UserToken(string username, string password, int companyId)
    {
        public string Username { get; private set; } = username;
        public string Password { get; private set; } = password;
        public int CompanyId { get; set; } = companyId;
    }
}
