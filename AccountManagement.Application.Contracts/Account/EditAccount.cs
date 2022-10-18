namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccount : RegisterAccount
    {
        public int Id { get; set; }

        public string Image { get; set; }
    }
}
