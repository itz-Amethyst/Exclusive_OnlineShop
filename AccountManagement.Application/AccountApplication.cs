using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication :IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthHelper _authHelper;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader, IAuthHelper authHelper)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
        }

        public OperationResult Create(RegisterAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var password = _passwordHasher.Hash(command.Password);

            var path = $"ProfilePhotos/{command.Username}";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto , path);

            var account = new Account(command.Username , password , command.Fullname , command.Mobile , command.RoleId , picturePath);

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname, account.Username);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Register(RegisterAccountByUser command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile))
            {
                return operation.Failed(ApplicationMessages.DuplicatedUser);
            }

            if (command.Password != command.RePassword)
            {
                return operation.Failed(ApplicationMessages.PasswordNotMatch);
            }


            var password = _passwordHasher.Hash(command.Password);

            var path = $"ProfilePhotos/{command.Username}";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);

            var account = new Account(command.Username, password, command.Fullname, command.Mobile, command.RoleId, picturePath);

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname, account.Username);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();

            _authHelper.SignIn(authViewModel);

            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetById(command.Id);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_accountRepository.Exists(x => x.Username == command.Username && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var path = $"ProfilePhotos/{command.Username}";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);

            account.Edit(command.Username, command.Fullname, command.Mobile, command.RoleId, picturePath);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();

            var account = _accountRepository.GetById(command.Id);

            if (account == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (command.Password != command.RePassword)
            {
                return operation.Failed(ApplicationMessages.PasswordNotMatch);
            }

            var password = _passwordHasher.Hash(command.Password);

            account.ChangePassword(password);

            _accountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();

            var account = _accountRepository.GetBy(command.UserName);

            if (account == null)
            {
                return operation.Failed(ApplicationMessages.WrongUsernameOrPassword);
            }

            (bool Verified , bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);

            if (!result.Verified)
            {
                return operation.Failed(ApplicationMessages.WrongUsernameOrPassword);
            }

            var authViewModel = new AuthViewModel(account.Id, account.RoleId ,account.Fullname , account.Username);

            _authHelper.SignIn(authViewModel);
            
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

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();

            var account = _accountRepository.GetById(id);

            if (account == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            account.Remove();
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();

            var account = _accountRepository.GetById(id);

            if (account == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            account.Restore();
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }
    }
}
