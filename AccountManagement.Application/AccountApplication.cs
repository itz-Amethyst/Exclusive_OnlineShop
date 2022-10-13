using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication :IAccountApplication
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
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
            var operation = new OperationResult();

            var account = _accountRepository.GetById(command.Id);

            if (account == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (account.Password != command.RePassword)
            {
                return operation.Failed(ApplicationMessages.PasswordNotMatch);
            }

            var password = _passwordHasher.Hash(command.Password);

            account.ChangePassword(password);

            _accountRepository.SaveChanges();

            return operation.Succeeded();
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
