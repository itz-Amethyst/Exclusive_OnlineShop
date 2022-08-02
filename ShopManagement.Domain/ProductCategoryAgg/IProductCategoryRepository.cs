using System.Linq.Expressions;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity); 
        
        ProductCategory GetById(int id);

        List<ProductCategory> GetAll();

        bool Exists(Expression<Func<ProductCategory , bool>> expression);

        void SaveChanges();

        EditProductCategory GetDetails(int id);
        
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
