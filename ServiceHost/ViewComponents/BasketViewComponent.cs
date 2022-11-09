using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
