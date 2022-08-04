using System.Linq.Expressions;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore.Context;

namespace ShopManagement.Infrastructure.EFCore.Repositories
{
    public class ProductCategoryRepository: RepositoryBase<int,ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context): base(context)
        {
            _context = context;
        }

        public EditProductCategory GetDetails(int id)
        {
            return _context.ProductCategories.Select(x => new EditProductCategory()
            {
                Id=x.Id,
                Description = x.Description,
                Name = x.Name,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).First(x => x.Id == id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
