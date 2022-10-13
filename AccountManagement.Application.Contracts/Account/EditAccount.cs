namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccount : CreateAccount
    {
        public int Id { get; set; }

        public string Image { get; set; }
    }
}
