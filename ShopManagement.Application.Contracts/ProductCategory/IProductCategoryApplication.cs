using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);

        OperationResult Edit(EditProductCategory command);

        Domain.ProductCategoryAgg.ProductCategory GetDetails(int id);

        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
