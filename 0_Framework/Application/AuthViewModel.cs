namespace _0_Framework.Application;

public class AuthViewModel
{
    public int Id { get; set; }

    public int RoleId { get; set; }
        
    public string FullName { get; set; }
        
    public string Username { get; set; }
    
    //public string Email { get; set; }
        
    //public string Picture { get; set; }
        
    //public List<long> Roles { get; set; }

    public AuthViewModel(int id, int roleId, string fullName, string username)
    {
        Id = id;
        RoleId = roleId;
        FullName = fullName;
        Username = username;
    }
}