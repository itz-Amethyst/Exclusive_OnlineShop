using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class UserSideBarViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
