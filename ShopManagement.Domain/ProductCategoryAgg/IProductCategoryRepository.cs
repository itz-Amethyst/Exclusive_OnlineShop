using System.Linq.Expressions;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity); 
        
        ProductCategory GetById(int id);

        List<ProductCategory> GetAll();

        bool Exists(Expression<Func<ProductCategory , bool>> expression);

        void SaveChanges();
    }
}
