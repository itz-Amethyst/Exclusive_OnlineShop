namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity); 
        
        ProductCategory GetById(int id);

        List<ProductCategory> GetAll();
    }
}
