using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Areas.Administration.Pages.Orders
{
    [PermissionChecker(Roles.ManageProductCategory)]
    public class IndexModel : PageModel
    {
        private readonly IOrderApplication _orderApplication;

        public OrderSearchModel SearchModel;
        public List<OrderViewModel> Orders;

        public IndexModel(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        public void OnGet(OrderSearchModel searchModel)
        {
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
