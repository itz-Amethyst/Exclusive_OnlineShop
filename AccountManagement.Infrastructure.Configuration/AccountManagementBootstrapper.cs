using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Infrastructure.EFCore.Context;
using AccountManagement.Infrastructure.EFCore.Repository;
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

            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
