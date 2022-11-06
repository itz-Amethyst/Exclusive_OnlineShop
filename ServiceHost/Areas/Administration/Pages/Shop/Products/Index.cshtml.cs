using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    [PermissionChecker(Roles.ManageShop)]
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        //[TempData] 
        //public string Message { get; set; }

        public ProductSearchModel SearchModel;
        public List<ProductViewModel> Products;
        public SelectList ProductCategories;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductSearchModel searchModel , bool removed = false , bool restored = false)
        {
            ViewData["Removed"] = removed;
            ViewData["Restored"] = restored;
            ProductCategories = new SelectList(_productCategoryApplication.GetProductsCategories(),"Id" , "Name");
            Products = _productApplication.Search(searchModel);
        }

        [PermissionChecker(Roles.CreateProduct)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories = _productCategoryApplication.GetProductsCategories()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }

        [PermissionChecker(Roles.EditProduct)]
        public IActionResult OnGetEdit(int id)
        {
            var product = _productApplication.GetDetails(id);
            product.Categories = _productCategoryApplication.GetProductsCategories();
            return Partial("Edit", product);
        }

        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);

            return new JsonResult(result);
        }

        [PermissionChecker(Roles.DeleteProduct)]
        public IActionResult OnGetRemove(int id)
        {
            var result = _productApplication.Remove(id);

            return RedirectToPage("./Index", new { Removed = "True" });

        }

        [PermissionChecker(Roles.DeleteProduct)]
        public IActionResult OnGetRestore(int id)
        {
            var result = _productApplication.Restore(id);

            return RedirectToPage("./Index", new { Restored = "True" });
        }
    }
}
