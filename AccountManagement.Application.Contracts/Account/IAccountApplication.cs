﻿using _0_Framework.Application;

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
        void Logout();
    }
}
