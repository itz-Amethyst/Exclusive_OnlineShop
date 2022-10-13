namespace AccountManagement.Application.Contracts.Account
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }

        public string Mobile { get; set; }

        public string Role  { get; set; }

        public int RoleId { get; set; }

        public string ProfilePhoto { get; set; }
    }
}
