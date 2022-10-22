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
        private readonly IViewRenderService _viewRender;
        

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader, IAuthHelper authHelper, IViewRenderService viewRender)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _viewRender = viewRender;
        }

        public OperationResult Create(RegisterAccount command)
        {
            var operation = new OperationResult();

            if(!_accountRepository.IsMobileNumberValid(command.Mobile))
            {
                return operation.Failed(ApplicationMessages.InvalidMobileNumber);
            }

            if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile  || x.Email == command.Email))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var password = _passwordHasher.Hash(command.Password);

            var path = $"ProfilePhotos/{command.Username}";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto , path);

            var activeCode = ActiveCodeGenerator.GenerateActiveCode();

            var account = new Account(command.Username, password, command.Mobile, command.RoleId, picturePath, activeCode, command.Email);

            var authViewModel = new AuthViewModel(account.Id , account.RoleId , account.Username , account.Email , false);

            account.ActivatedAccount();

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Register(RegisterAccountByUser command)
        {
            var operation = new OperationResult();
            
            if (!_accountRepository.IsMobileNumberValid(command.Mobile))
            {
                return operation.Failed(ApplicationMessages.InvalidMobileNumber);
            }

            if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile || x.Email == command.Email))
            {
                return operation.Failed(ApplicationMessages.DuplicatedUser);
            }

            if (command.Password != command.RePassword)
            {
                return operation.Failed(ApplicationMessages.PasswordNotMatch);
            }

            
            var password = _passwordHasher.Hash(command.Password);

            var path = $"ProfilePhotos/{command.Username}/avatar-9.jpg";
            //var picturePath = _fileUploader.Upload(,path);

            var account = new Account(command.Username, password, command.Mobile, command.RoleId, path, _accountRepository.GenerateActiveCodeUser(), command.Email);

            //!Activation Section
            string body = _viewRender.RenderToStringAsync("_ActiveEmail", account);

            SendEmail.Send(command.Email, "فعالسازی", body);

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Username, account.Email , true);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();

            //_authHelper.SignIn(authViewModel);

            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            
            if (!_accountRepository.IsMobileNumberValid(command.Mobile))
            {
                return operation.Failed(ApplicationMessages.InvalidMobileNumber);
            }

            var account = _accountRepository.GetById(command.Id);
            if (account == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_accountRepository.Exists(x => x.Username == command.Username && x.Id != command.Id || x.Mobile == command.Mobile || x.Email == command.Email))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var path = $"ProfilePhotos/{command.Username}";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);

            account.Edit(command.Username, command.Mobile, command.RoleId, picturePath, command.Email);
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

            var account = _accountRepository.GetBy(command.UserNameOrEmail);

            if (account == null)
            {
                return operation.Failed(ApplicationMessages.WrongUsernameOrPassword);
            }

            if (!account.IsActive)
            {
                return operation.Failed(ApplicationMessages.AccountIsNotActive);
            }

            (bool Verified , bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);

            if (!result.Verified)
            {
                return operation.Failed(ApplicationMessages.WrongUsernameOrPassword);
            }

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Username, account.Email, command.RememberMe);

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

        public bool ActiveAccount(string activeCode)
        {
            var account = _accountRepository.GetByActiveCode(activeCode);

            if (account == null || account.IsActive)
            {
                return false;
            }

            account.ActivatedAccount();
            
            //! For 1 time uses
            account.ActiveCode = ActiveCodeGenerator.GenerateActiveCode();

            _accountRepository.SaveChanges();

            var user = new AuthViewModel(account.Id, account.RoleId, account.Username, account.Email, true);

            _authHelper.SignIn(user);
            
            return true;
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }
    }
}
