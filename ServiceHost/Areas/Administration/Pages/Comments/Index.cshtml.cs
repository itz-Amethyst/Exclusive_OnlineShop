using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Security;
using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Comment;

namespace ServiceHost.Areas.Administration.Pages.Comments
{
    [PermissionChecker(Roles.ManageComment)]
    public class IndexModel : PageModel
    {
        private readonly ICommentApplication _commentApplication;
      

        public List<CommentViewModel> Comments;
        public CommentSearchModel SearchModel;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public void OnGet(CommentSearchModel searchModel, bool confirmed = false , bool canceled = false)
        {
            ViewData["Confirmed"] = confirmed;
            ViewData["Canceled"] = canceled;
            Comments = _commentApplication.Search(searchModel);
        }

        [PermissionChecker(Roles.AccessToCommentOperations)]
        public IActionResult OnGetCancel(int id)
        {
            var result = _commentApplication.Cancel(id);
           
            return RedirectToPage("./Index" , new {Canceled="True"});
            
        }

        [PermissionChecker(Roles.AccessToCommentOperations)]
        public IActionResult OnGetConfirm(int id)
        {
            var result = _commentApplication.Confirm(id);

            return RedirectToPage("./Index", new { Confirmed = "True" });
        }
    }
}
