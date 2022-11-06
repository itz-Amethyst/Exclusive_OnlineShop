using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Security;
using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscounts
{
    [PermissionChecker(Roles.ManageCustomerDiscount)]
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;


        public CustomerDiscountSearchModel SearchModel;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public SelectList Products;

        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            _productApplication = productApplication;
            _customerDiscountApplication = customerDiscountApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(),"Id" , "Name");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }

        [PermissionChecker(Roles.CreateCustomerDiscount)]
        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }
        
        [PermissionChecker(Roles.EditCustomerDiscount)]
        public IActionResult OnGetEdit(int id)
        {
            var customerDiscount  = _customerDiscountApplication.GetDetails(id);
            customerDiscount.Products = _productApplication.GetProducts();
            return Partial("Edit", customerDiscount);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Edit(command);

            return new JsonResult(result);
        }

    }
}
