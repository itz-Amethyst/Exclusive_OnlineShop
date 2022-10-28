using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account.Admin;
using AccountManagement.Application.Contracts.Account.User;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Create(RegisterAccount command);

        OperationResult Register(RegisterAccountByUser command);
        
        OperationResult Edit(EditAccount command);

        OperationResult ChangePassword(ChangePassword command);

        OperationResult Login(Login command);

        EditAccount GetDetails(int id);
        
        List<AccountViewModel> Search(AccountSearchModel searchModel);

        OperationResult Remove(int id);

        OperationResult Restore(int id);

        bool ActiveAccount(string activeCode);

        void Logout();

        OperationResult SendForgotPasswordLink(string email);
        
        OperationResult ResetPassword(ResetPasswordViewModel command);

        AccountInformationViewModel GetUserInformation(string username);

        UserPanelSideBarViewModel GetUserSideBarData(string username);

        EditUserProfile GetProfileDetails(string username);
    }
}
