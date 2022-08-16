using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.SlideAgg;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        private readonly ISlideApplication _slideApplication;
      

        public List<SlideViewModel> Slides;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet(bool restored = false , bool removed = false)
        {
            ViewData["Restored"] = restored;
            ViewData["Removed"] = removed;
            Slides = _slideApplication.GetList();
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = _slideApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(int id)
        {
            var slide = _slideApplication.GetDetails(id);
            return Partial("Edit", slide);
        }

        public JsonResult OnPostEdit(EditSlide command)
        {
            var result = _slideApplication.Edit(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(int id)
        {
            var result = _slideApplication.Remove(id);
           
            return RedirectToPage("./Index" , new {Removed="True"});
            
        }

        public IActionResult OnGetRestore(int id)
        {
            var result = _slideApplication.Restore(id);

            return RedirectToPage("./Index", new { Restored = "True" });
        }
    }
}
