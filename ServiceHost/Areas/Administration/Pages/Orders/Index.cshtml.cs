using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Infrastructure.EFCore.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Areas.Administration.Pages.Orders
{
    [PermissionChecker(Roles.AccessToOrdersSection)]
    public class IndexModel : PageModel
    {
        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;

        public OrderSearchModel SearchModel;
        public List<OrderViewModel> Orders;
        public SelectList Accounts;

        public IndexModel(IOrderApplication orderApplication, IAccountApplication accountApplication)
        {
            _orderApplication = orderApplication;
            _accountApplication = accountApplication;
        }

        public void OnGet(OrderSearchModel searchModel , bool confirmed , bool canceled)
        {
            ViewData["Confirmed"] = confirmed;
            ViewData["Canceled"] = canceled;

            Accounts = new SelectList(_accountApplication.GetAccounts(), "Id", "Username");
            Orders = _orderApplication.Search(searchModel);
        }

        public IActionResult OnGetConfirm(int id)
        {
            _orderApplication.PaymentSucceeded(id, 0);
            return RedirectToPage("./Index", new { Confirmed = "True" });
        }

        public IActionResult OnGetCancel(int id)
        {
            _orderApplication.Cancel(id);
            return RedirectToPage("./Index", new { Confirmed = "True" });
        }


        //[PermissionChecker(Roles.CreateProductCategory)]
        //public IActionResult OnGetCreate()
        //{
        //    return Partial("./Create", new CreateProductCategory());
        //}

        //public JsonResult OnPostCreate(CreateProductCategory command)
        //{
        //    var result = _orderApplication.Create(command);
        //    return new JsonResult(result);
        //}
    }
}
