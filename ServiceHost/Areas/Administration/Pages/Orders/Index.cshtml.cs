using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Infrastructure.EFCore.Security;
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

        public void OnGet(OrderSearchModel searchModel)
        {
            Accounts = new SelectList(_accountApplication.GetAccounts(), "Id", "Username");
            Orders = _orderApplication.Search(searchModel);
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
