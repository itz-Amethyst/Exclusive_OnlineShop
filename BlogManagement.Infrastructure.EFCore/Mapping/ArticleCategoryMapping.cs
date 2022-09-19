﻿using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mapping
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(2000);
            builder.Property(x => x.Picture).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Keywords).HasMaxLength(500).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(500).IsRequired();
            builder.Property(x => x.CanonicalAddress).HasMaxLength(500);

        }
    }
}
