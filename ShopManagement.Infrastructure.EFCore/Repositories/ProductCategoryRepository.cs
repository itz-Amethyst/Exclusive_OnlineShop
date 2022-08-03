using System.Linq.Expressions;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore.Context;

namespace ShopManagement.Infrastructure.EFCore.Repositories
{
    public class ProductCategoryRepository:IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context)
        {
            _context = context;
        }

        public void Create(ProductCategory entity)
        { 
            _context.ProductCategories.Add(entity);
        }

        public ProductCategory GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductCategory> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<ProductCategory, bool>> expression)
        {
            return _context.ProductCategories.Any(expression);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public EditProductCategory GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
