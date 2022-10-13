using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication :IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;

        public AccountApplication(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public OperationResult Create(CreateAccount command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Edit(EditAccount command)
        {
            throw new NotImplementedException();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            throw new NotImplementedException();
        }

        public EditAccount GetDetails(int id)
        {
            return _accountRepository.GetDetails(id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }
    }
}
