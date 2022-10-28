using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Application.Contracts.Account.User;
using AccountManagement.Application.Contracts.Account.Admin;

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

        public OperationResult SendForgotPasswordLink(string email)
        {
            var operation = new OperationResult();

            var user = _accountRepository.GetByEmail(email);

            if (user == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            //!Activation Section
            string body = _viewRender.RenderToStringAsync("_ForgotPassword", user);

            SendEmail.Send(user.Email, "بازیابی رمز عبور", body);

            return operation.Succeeded(ApplicationMessages.ForgotPasswordLinkSent);
        }

        public OperationResult ResetPassword(ResetPasswordViewModel command)
        {
            var operation = new OperationResult();
            
            var user = _accountRepository.GetByActiveCode(command.ActiveCode);

            if (user == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            var checkPassword = _passwordHasher.Check(user.Password, command.Password);

            if (checkPassword.Verified)
            {
                return operation.Failed(ApplicationMessages.PasswordIsSame);
            }

            if (command.Password != command.RePassword)
            {
                return operation.Failed(ApplicationMessages.PasswordNotMatch);
            }

            var password = _passwordHasher.Hash(command.Password);

            user.ChangePassword(password);

            user.ActiveCode = _accountRepository.GenerateActiveCodeUser();

            _accountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public AccountInformationViewModel GetUserInformation(string username)
        {
            var account = _accountRepository.GetByUserName(username);


            var information = new AccountInformationViewModel
            {
                Email = account.Email,
                Mobile = account.Mobile,
                ProfilePhoto = account.ProfilePhoto,
                RegisterDate = account.CreationDate.ToFarsi(),
                Username = account.Username
            };

            return information;
        }

        public UserPanelSideBarViewModel GetUserSideBarData(string username)
        {
            var user = _accountRepository.GetByUserName(username);

            var sideBarData = new UserPanelSideBarViewModel
            {
                 //= user.ProfilePhoto,
                UserName = user.Username,
                RegisterDate = user.CreationDate.ToFarsi(),
                UserProfilePicture = user.ProfilePhoto
            };

            return sideBarData;
        }

        public EditUserProfile GetProfileDetails(string username)
        {
            return _accountRepository.GetProfileDetails(username);
        }
    }
}
