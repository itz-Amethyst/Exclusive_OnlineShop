using _01_ExclusiveQuery.Contracts.ProductCategory;
using _01_ExclusiveQuery.Contracts.Slide;
using _01_ExclusiveQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore.Context;
using ShopManagement.Infrastructure.EFCore.Repositories;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services , string connectionString)
        {
            #region ProductCategory

            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            #endregion

            #region Product

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            #endregion

            #region ProductPicture

            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            #endregion

            #region Slide

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();

            #endregion

            #region SlideQuery

            services.AddTransient<ISlideQuery, SlideQuery>();

            #endregion

            #region ProductCategoryQuer

            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

            #endregion

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}