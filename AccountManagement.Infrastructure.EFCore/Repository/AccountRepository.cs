using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account.Admin;
using AccountManagement.Application.Contracts.Account.User;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<int, Account>, IAccountRepository
    {
        private readonly AccountContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AccountRepository(AccountContext context, IPasswordHasher passwordHasher) : base(context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public EditAccount GetDetails(int id)
        {
            return _context.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                Username = x.Username,
                Email = x.Email,
                Mobile = x.Mobile,
                RoleId = x.RoleId,
                Image = x.ProfilePhoto
            }).First(x => x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts
                .Include(x => x.Role)
                .Select(x => new AccountViewModel
                {
                    Id = x.Id,
                    Username = x.Username,
                    Mobile = x.Mobile,
                    ProfilePhoto = x.ProfilePhoto,
                    Role = x.Role.Name,
                    RoleId = x.RoleId,
                    CreationDate = x.CreationDate.ToFarsi(),
                    IsDeleted = x.IsRemoved,
                    Email = x.Email
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Username))
            {
                query = query.Where(x => x.Username.Contains(searchModel.Username));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            {
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));
            }

            if (searchModel.RoleId > 0)
            {
                query = query.Where(x => x.RoleId == searchModel.RoleId);
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public Account GetBy(string usernameOrEmail)
        {
            return _context.Accounts.FirstOrDefault(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail);
        }

        public string GenerateActiveCodeUser()
        {
            return ActiveCodeGenerator.GenerateActiveCode();
        }

        public Account GetByActiveCode(string activeCode)
        {
            return _context.Accounts.FirstOrDefault(x => x.ActiveCode == activeCode);
        }

        public bool IsMobileNumberValid(string mobile)
        {
            return MobileNumberValidator.IsMobileNumberValid(mobile);
        }

        public Account GetByEmail(string email)
        {
            var input = email.Trim().ToLower();

            return _context.Accounts.FirstOrDefault(x => x.Email == input);
        }

        public Account GetByUserName(string username)
        {
            return _context.Accounts.FirstOrDefault(x => x.Username == username);
        }

        public EditUserProfile GetProfileDetails(string username)
        {
            return _context.Accounts.Select(x => new EditUserProfile
            {
                Id = x.Id,
                UserName = x.Username,
                Email = x.Email,
                Mobile = x.Mobile,
                Image = x.ProfilePhoto,
            }).First(x => x.UserName == username);
        }

        public bool CheckOldPassword(string oldPassword, string username)
        {
            string hashOldPassword = _passwordHasher.Hash(oldPassword);
            return _context.Accounts.Any(u => u.Username == username && u.Password == hashOldPassword);
        }
    }
}
