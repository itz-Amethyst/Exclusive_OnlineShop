using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.Order.UserPanel;

namespace ServiceHost.Areas.UserPanel.Pages.Orders
{
    public class ShowOrderModel : PageModel
    {
        private readonly IOrderApplication _orderApplication;

        public List<ShowUserOrderViewModel> OrderDetail;
        public SummaryOrderSectionViewModel SummaryOrder;

        public ShowOrderModel(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        public void OnGet(int orderId)
        {
            OrderDetail = _orderApplication.GetOrderDetails(orderId);
            SummaryOrder = _orderApplication.GetOrderSummary(orderId);
        }
    }
}
