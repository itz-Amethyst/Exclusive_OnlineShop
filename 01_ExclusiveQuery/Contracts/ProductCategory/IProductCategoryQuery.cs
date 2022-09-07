namespace _01_ExclusiveQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug);

        List<ProductCategoryQueryModel> GetProductCategories();

        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}
