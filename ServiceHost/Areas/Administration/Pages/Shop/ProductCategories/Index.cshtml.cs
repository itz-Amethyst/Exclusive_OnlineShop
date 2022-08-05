using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication _productCategoryApplication;

        public List<ProductCategoryViewModel> ProductCategories;

        public IndexModel(IProductCategoryApplication productCategoryApplication, List<ProductCategoryViewModel> productCategories)
        {
            _productCategoryApplication = productCategoryApplication;
            ProductCategories = productCategories;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }
    }
}
