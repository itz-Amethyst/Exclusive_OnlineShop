using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Security;
using DiscountManagement.Application.Contract.CouponDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CouponDiscounts
{
    [PermissionChecker(Roles.ManageCustomerDiscount)]
    public class IndexModel : PageModel
    {
        private readonly ICouponDiscountApplication _couponDiscountApplication;

        public CouponDiscountSearchModel SearchModel;
        public List<CouponDiscountViewModel> CouponDiscounts;

        public IndexModel(ICouponDiscountApplication couponDiscountApplication)
        {
            _couponDiscountApplication = couponDiscountApplication;
        }

        public void OnGet(CouponDiscountSearchModel searchModel , bool removed = false , bool restored = false)
        {
            ViewData["Restored"] = restored;
            ViewData["Removed"] = removed;
            CouponDiscounts = _couponDiscountApplication.Search(searchModel);
        }

        [PermissionChecker(Roles.CreateCustomerDiscount)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }

        public JsonResult OnPostCreate(DefineCouponDiscount command)
        {
            var result = _couponDiscountApplication.Define(command);
            return new JsonResult(result);
        }
        
        [PermissionChecker(Roles.EditCustomerDiscount)]
        public IActionResult OnGetEdit(int id)
        {
            var customerDiscount  = _couponDiscountApplication.GetDetails(id);
            return Partial("Edit", customerDiscount);
        }

        public JsonResult OnPostEdit(EditCouponDiscount command)
        {
            var result = _couponDiscountApplication.Edit(command);

            return new JsonResult(result);
        }

        [PermissionChecker(Roles.DeleteCustomerDiscount)]
        public IActionResult OnGetRemove(int id)
        {
            var result = _couponDiscountApplication.Remove(id);

            return RedirectToPage("./Index", new { Removed = "True" });
        }

        [PermissionChecker(Roles.DeleteCustomerDiscount)]
        public IActionResult OnGetRestore(int id)
        {
            var result = _couponDiscountApplication.Restore(id);

            return RedirectToPage("./Index", new { Restored = "True" });
        }
    }
}
