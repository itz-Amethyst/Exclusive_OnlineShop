using DiscountManagement.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Context;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Configuration
{
    public class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            #region CustomerDiscount

            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            #endregion

            services.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}