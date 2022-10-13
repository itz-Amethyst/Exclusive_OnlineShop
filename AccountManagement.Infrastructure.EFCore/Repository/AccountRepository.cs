using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<int , Account> , IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public EditAccount GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
