using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;

        public ProductSearchModel SearchModel;
        public List<ProductViewModel> Products;

        public IndexModel(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            Products = _productApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProduct());
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(int id)
        {
            var product = _productApplication.GetDetails(id);

            return Partial("Edit", product);
        }

        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);

            return new JsonResult(result);
        }
    }
}
