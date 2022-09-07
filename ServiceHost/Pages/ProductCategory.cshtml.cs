using _01_ExclusiveQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        public ProductCategoryQueryModel ProductCategory;
        private readonly IProductCategoryQuery _productcategoryQuery;

        public ProductCategoryModel(IProductCategoryQuery productcategoryQuery)
        {
            _productcategoryQuery = productcategoryQuery;
        }

        public void OnGet(string id)
        {
            ProductCategory = _productcategoryQuery.GetProductCategoryWithProductsBy(id);
        }
    }
}
