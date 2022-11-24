using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account.Admin;
using AccountManagement.Application.Contracts.Account.User;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<int , Account>
    {
        EditAccount GetDetails(int id);

        List<AccountViewModel> Search(AccountSearchModel searchModel);

        Account GetBy(string usernameOrEmail);

        string GenerateActiveCodeUser();
        
        Account GetByActiveCode(string activeCode);

        bool IsMobileNumberValid(string mobile);

        Account GetByEmail(string email);

        Account GetByUserName(string username);

        EditUserProfile GetProfileDetails(string username);

        bool CheckPassword(string databasePassword, string userPassword);

        List<AccountViewModel> GetAccounts();
    }
}
