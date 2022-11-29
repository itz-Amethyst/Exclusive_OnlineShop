using DiscountManagement.Domain.ColleagueDiscountsAgg;
using DiscountManagement.Domain.CouponDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure.EFCore.Context
{
    public class DiscountContext : DbContext
    {
        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        public DbSet<ColleagueDiscount> ColleagueDiscounts { get; set; }
        public DbSet<CouponDiscount> DiscountsCoupon { get; set; }
        public DbSet<DiscountUsage> DiscountUsages { get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CustomerDiscountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
