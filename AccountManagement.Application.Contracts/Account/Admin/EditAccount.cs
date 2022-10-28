namespace AccountManagement.Application.Contracts.Account.Admin
{
    public class EditAccount : RegisterAccount
    {
        public int Id { get; set; }

        public string Image { get; set; }
    }
}
