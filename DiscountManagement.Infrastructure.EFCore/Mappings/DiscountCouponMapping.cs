using DiscountManagement.Domain.CouponDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EFCore.Mappings
{
    public class DiscountCouponMapping : IEntityTypeConfiguration<CouponDiscount>
    {
        public void Configure(EntityTypeBuilder<CouponDiscount> builder)
        {
            builder.ToTable("Discounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DiscountCode).IsRequired().HasMaxLength(12);

            builder.OwnsOne(x => x.DisUsage, navigationBuilder =>
            {
                navigationBuilder.ToTable("DiscountUsages");
                navigationBuilder.HasKey(x => x.Id);
                navigationBuilder.WithOwner(x => x.CouponDiscount).HasForeignKey(x => x.Id);
            });
        }
    }
}
