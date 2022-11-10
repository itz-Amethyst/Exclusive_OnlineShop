using _0_Framework.Application;
using _01_ExclusiveQuery.Contracts.Comment;
using _01_ExclusiveQuery.Contracts.Product;
using BlogManagement.Domain.ArticleAgg;
using CommentManagement.Infrastructure.EFCore.Context;
using DiscountManagement.Infrastructure.EFCore.Context;
using InventoryManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore.Context;

namespace _01_ExclusiveQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, CommentContext commentContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var date = _shopContext.Products.Select(x => new { x.CreationDateNewLabel, x.Id }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.IsDeleted == false)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var products = _shopContext.Products
                .Include(x => x.Category)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    CategorySlug = product.Category.Slug,
                    ProductCategory = product.Category.Name,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                    IsDeleted = product.IsDeleted
                }).AsNoTracking().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).Take(6).ToList();


            foreach (var product in products)
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


            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var date = _shopContext.Products.Select(x => new { x.CreationDateNewLabel, x.Id }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.IsDeleted == false)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var query = _shopContext.Products
                .Include(x => x.Category)
                .Select(product => new ProductQueryModel()
                {
                    Id = product.Id,
                    ProductCategory = product.Category.Name,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                    IsDeleted = product.IsDeleted,
                    ShortDescription = product.ShortDescription
                }).Where(x => x.IsDeleted == false).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
            {
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));
            }

            var products = query.OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
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

            return products;
        }

        public ProductQueryModel GetProductDetails(string slug)
        {
            var inventory = _inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice  , x.InStock}).ToList();

            var date = _shopContext.Products.Select(x => new { x.CreationDateNewLabel, x.Id }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.IsDeleted == false)
                .Select(x => new { x.ProductId, x.DiscountRate , x.EndDate }).ToList();

            var product = _shopContext.Products
                .Include(x => x.Category)
                .Include(x=> x.ProductPictures)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    ProductCategory = product.Category.Name,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                    IsDeleted = product.IsDeleted,
                    CategorySlug = product.Category.Slug,
                    Code = product.Code,
                    Description = product.Description,
                    Keywords = product.Keywords,
                    MetaDescription = product.MetaDescription,
                    ShortDescription = product.ShortDescription,
                    Pictures = MapProductPictures(product.ProductPictures)
                }).AsNoTracking().Where(x => !x.IsDeleted).FirstOrDefault(x => x.Slug == slug);

            if (product == null)
            {
                return new ProductQueryModel();
            }

            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);

            if (productInventory != null)
            {
                product.IsInStock = productInventory.InStock;
                
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                product.PriceWithDouble = price;
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

            var comments = _commentContext.Comments
                .Where(x => x.Type == CommentTypes.Product)
                .Where(x => x.OwnerRecordId == product.Id)
                .Where(x => !x.IsCanceled && x.IsConfirmed)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ParentId = x.ParentId,
                }).ToList();

            foreach (var comment in comments)
            {
                if (comment.ParentId > 0)
                {
                    comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
                }
            }

            product.Comments = comments;

            return product;
        }

        //private static List<CommentQueryModel> MapComments(List<Comment> productComments)
        //{
        //    return productComments.Where(x => !x.IsCanceled && x.IsConfirmed)
        //        .Select(x => new CommentQueryModel
        //        {
        //            Id = x.Id,
        //            Message = x.Message,
        //            Name = x.Name,
        //            CreationDate = x.CreationDate.ToFarsi()
        //        }).OrderByDescending(x=>x.Id).ToList();
        //}

        private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> picture)
        {
            return picture.Select(x => new ProductPictureQueryModel
            {
                IsRemoved = x.IsRemoved,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).Where(x => !x.IsRemoved).ToList();
        }
    }
}
