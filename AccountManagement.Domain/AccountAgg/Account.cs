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

        //! It's not private cause we need to use it in application and change it ActiveAccount Method for 1 time uses
        public string ActiveCode { get; set; }

        public string Email { get; private set; }

        public bool IsActive { get; private set; }

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
            IsActive = false;
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

        public void EditUserPanel(string username, string mobile, string profilePhoto, string email)
        {
            Username = username;
            Mobile = mobile;
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

        public void ActivatedAccount()
        {
            IsActive = true;
        }

        public void DeActiveAccount()
        {
            IsActive = false;
        }
    }
}
