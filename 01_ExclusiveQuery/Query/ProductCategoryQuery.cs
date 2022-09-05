using _0_Framework.Application;
using _01_ExclusiveQuery.Contracts.Product;
using _01_ExclusiveQuery.Contracts.ProductCategory;
using InventoryManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore.Context;

namespace _01_ExclusiveQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;

        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            return _shopContext.ProductCategories.Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).ToList();
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                ProductCategory = product.Category.Name,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
                IsDeleted = product.IsDeleted
            }).Where(x => x.IsDeleted == false).ToList();
            
        }
    }
}
