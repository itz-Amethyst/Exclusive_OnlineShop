using _01_ExclusiveQuery.Contracts.Product;

namespace _01_ExclusiveQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetProductCategories();

        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}
