using System.Linq.Expressions;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore.Context;

namespace ShopManagement.Infrastructure.EFCore.Repositories
{
    public class ProductRepository : RepositoryBase<int , Product> , IProductRepository
    {
        private readonly  ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProduct GetDetails(int id)
        {
            return _context.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Slug = x.Slug,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                UnitPrice = x.UnitPrice,
            }).First(x => x.Id == id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products.Include(x => x.Category).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Picture = x.Picture,
                UnitPrice = x.UnitPrice,
                CategoryName = x.Category.Name,
                CategoryId = x.CategoryId
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
            {
                query = query.Where(x => x.Code.Contains(searchModel.Code));
            }

            if (searchModel.CategoryId != 0)
            {
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
