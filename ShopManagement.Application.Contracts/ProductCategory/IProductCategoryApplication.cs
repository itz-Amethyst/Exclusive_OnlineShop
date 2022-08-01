namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        void Create(CreateProductCategory command);

        void Edit(EditProductCategory command);

        Domain.ProductCategoryAgg.ProductCategory GetDetails(int id);

        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
