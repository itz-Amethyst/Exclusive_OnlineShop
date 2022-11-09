using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    [PermissionChecker(Roles.ManageProductCategory)]
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication _productCategoryApplication;

        public ProductCategorySearchModel SearchModel;
        public List<ProductCategoryViewModel> ProductCategories;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }

        [PermissionChecker(Roles.CreateProductCategory)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        [PermissionChecker(Roles.EditProductCategory)]
        public IActionResult OnGetEdit(int id)
        {
            var productCategory = _productCategoryApplication.GetDetails(id);

            return Partial("Edit", productCategory);
        }

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = _productCategoryApplication.Edit(command);

            return new JsonResult(result);
        }
    }
}
