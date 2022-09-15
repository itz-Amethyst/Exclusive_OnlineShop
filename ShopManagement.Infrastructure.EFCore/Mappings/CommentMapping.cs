﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EFCore.Mappings
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Message).HasMaxLength(100).IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
