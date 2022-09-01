using _01_ExclusiveQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponents : ViewComponent
    {
        private readonly ISlideQuery _slidQuery;

        public SlideViewComponents(ISlideQuery slideQuery)
        {
            _slidQuery = slideQuery;
        }

        public IViewComponentResult Invoke()
        {
            var slides = _slidQuery.GetSlides();

            return View("/Pages/Shared/Components/Slide/Default.cshtml", slides);
        }
    }
}
