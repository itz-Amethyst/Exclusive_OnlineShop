using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.Order.UserPanel;

namespace ServiceHost.Areas.UserPanel.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderApplication _orderApplication;
        private readonly IAuthHelper _authHelper;

        public List<UserOrdersViewModel> Orders;

        public IndexModel(IOrderApplication orderApplication, IAuthHelper authHelper)
        {
            _orderApplication = orderApplication;
            _authHelper = authHelper;
        }

        public void OnGet()
        {
            var accountId = _authHelper.CurrentAccountId();

            Orders = _orderApplication.GetUserOrders(accountId);
        }
    }
}
