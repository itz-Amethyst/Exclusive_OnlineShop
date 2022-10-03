using _01_ExclusiveQuery.Contracts.Article;
using _01_ExclusiveQuery.Contracts.ArticleCategory;
using _01_ExclusiveQuery.Query;
using BlogManagement.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore.Context;
using BlogManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Infrastructure.Configuration
{
    public class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            #region ArticleCategory

            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();

            #endregion

            #region Article

            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleRepository, ArticleRepository>();

            #endregion

            #region ArticleQuery

            services.AddTransient<IArticleQuery,ArticleQuery>();
            services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();

            #endregion

            services.AddDbContext<BlogContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
