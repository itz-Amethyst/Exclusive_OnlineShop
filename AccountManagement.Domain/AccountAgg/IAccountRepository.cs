using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<int , Account>
    {
        EditAccount GetDetails(int id);

        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
