using _0_Framework.Application;
using _01_ExclusiveQuery.Contracts.Product;
using _01_ExclusiveQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore.Context;
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
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
        {
            var inventory = _inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var date = _shopContext.Products.Select(x => new { x.CreationDateNewLabel, x.Id }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.IsDeleted == false)
                .Select(x => new { x.ProductId, x.DiscountRate , x.EndDate }).ToList();

            var category = _shopContext.ProductCategories.Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    Slug = x.Slug,
                    Products = MapProducts(x.Products)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);


            foreach (var product in category.Products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);

                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();
                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);

                    var productDate = date.FirstOrDefault(x => x.Id == product.Id);

                    var productLabelDate = product.LabelDate = productDate.CreationDateNewLabel;

                    var currentDate = DateTime.Now;

                    product.IsNew = currentDate <= productLabelDate;

                    if (discount != null)
                    {
                        int discountRate = discount.DiscountRate;

                        product.DiscountRate = discountRate;

                        product.DiscountEndDate = discount.EndDate.ToDiscountFormat();

                        product.HasDiscount = discountRate > 0;

                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }

            return category;
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
            }).AsNoTracking().ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var date = _shopContext.Products.Select(x => new { x.CreationDateNewLabel, x.Id }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.IsDeleted == false)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var categories = _shopContext.ProductCategories.Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).AsNoTracking().ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);

                    if (productInventory != null)
                    {
                        var price = productInventory.UnitPrice;
                        product.Price = price.ToMoney();
                        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);

                        var productDate = date.FirstOrDefault(x => x.Id == product.Id);

                        var productLabelDate = product.LabelDate = productDate.CreationDateNewLabel;

                        var currentDate = DateTime.Now;

                        product.IsNew = currentDate <= productLabelDate;

                        if (discount != null)
                        {
                            int discountRate = discount.DiscountRate;

                            product.DiscountRate = discountRate;
                            product.HasDiscount = discountRate > 0;

                            var discountAmount = Math.Round((price * discountRate) / 100);
                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }
                    }

                }
            }

            return categories;
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                ProductCategory = product.Category.Name,
                CategorySlug = product.Category.Slug,
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
