﻿namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SignIn(AuthViewModel account);

        void SignOut();

        bool IsAuthenticated();

        //string CurrentUserRole();

        //AuthViewModel CurrentAccountInfo();

        int CurrentAccountId();

        string CurrentAccountUserName();

        string CurrentAccountEmail();

        string CurrentAccountMobile();
    }
}
