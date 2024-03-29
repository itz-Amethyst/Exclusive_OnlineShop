﻿using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EFCore.Context;
using AccountManagement.Infrastructure.EFCore.Repository;
using AccountManagement.Infrastructure.EFCore.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            #region Account

            services.AddTransient<IAccountApplication, AccountApplication>();

            services.AddTransient<IAccountRepository, AccountRepository>();

            #endregion

            #region Role

            services.AddTransient<IRoleApplication, RoleApplication>();

            services.AddTransient<IRoleRepository, RoleRepository>();

            #endregion

            #region PermissionChecker

            services.AddTransient<IPermissionChecker, PermissionChecker>();

            #endregion

            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
