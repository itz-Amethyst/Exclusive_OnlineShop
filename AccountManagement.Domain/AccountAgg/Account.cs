using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string Username { get; private set; }
        
        public string Password { get; private set; }
        
        public string Mobile { get; private set; }
        
        public int RoleId { get; private set; }

        public Role Role { get; private set; }
        
        public bool IsRemoved { get; private set; }

        public string ProfilePhoto { get; private set; }

        public string ActiveCode { get; private set; }

        public string Email { get; private set; }

        public Account(string username, string password, string mobile, int roleId, string profilePhoto , string activeCode , string email)
        {
            Username = username;
            Password = password;
            Mobile = mobile;
            RoleId = roleId;

            if (roleId == 0)
            {
                RoleId = 3;
            }
            
            IsRemoved = false;
            ProfilePhoto = profilePhoto;
            ActiveCode = activeCode;
            Email = email;
        }

        public void Edit(string username, string mobile, int roleId, string profilePhoto , string email)
        {
            Username = username;
            Mobile = mobile;
            RoleId = roleId;
            if (!string.IsNullOrWhiteSpace(profilePhoto))
            {
                ProfilePhoto = profilePhoto;
            }
            Email = email;

        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
