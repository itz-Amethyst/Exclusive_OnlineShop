namespace AccountManagement.Application.Contracts.Account
{
    public class ChangePassword
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public string RePassword { get; set; }
    }
}
