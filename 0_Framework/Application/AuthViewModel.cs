namespace _0_Framework.Application;

public class AuthViewModel
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public bool RememberMe { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    //public string Picture { get; set; }

    //public List<long> Roles { get; set; }

    public string Role { get; set; }

    public AuthViewModel()
    {
    }

    public AuthViewModel(int id, int roleId, string username, string email , bool rememberMe)
    {
        Id = id;
        RoleId = roleId;
        Username = username;
        Email = email;
        RememberMe = rememberMe;
    }
    
}