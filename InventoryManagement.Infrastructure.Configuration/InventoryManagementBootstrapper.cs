using _01_ExclusiveQuery.Contracts.Inventory;
using _01_ExclusiveQuery.Query;
using InventoryManagement.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore.Context;
using InventoryManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            #region Inventory

            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();

            #endregion

            #region InventoryQuery

            services.AddTransient<IInventoryQuery , InventoryQuery>();

            #endregion

            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
        }
    }
}