using _01_ExclusiveQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponent : ViewComponent
    {
        private readonly ISlideQuery _slidQuery;

        public SlideViewComponent(ISlideQuery slideQuery)
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
