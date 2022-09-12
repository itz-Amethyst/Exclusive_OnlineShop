using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository: IRepository<int , ProductCategory>
    {
        List<ProductCategoryViewModel> GetProductCategories();

        EditProductCategory GetDetails(int id);
        
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

        string GetSlugById(int id);
    }
}
