using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Security;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    [PermissionChecker(Roles.ManageArticle)]
    public class IndexModel : PageModel
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        
        public ArticleSearchModel SearchModel;
        public List<ArticleViewModel> Articles;
        public SelectList ArticleCategories;

        public IndexModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleSearchModel searchModel , bool created = false , bool edited = false , bool activated = false , bool deActivated = false)
        {
            ViewData["Created"] = created;
            
            ViewData["Edited"] = edited;
            
            ViewData["DeActivated"] = deActivated;

            ViewData["Activated"] = activated;

            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");

            Articles = _articleApplication.Search(searchModel);
        }

        [PermissionChecker(Roles.DeleteArticle)]
        public IActionResult OnGetDeActive(int id)
        {
            var result = _articleApplication.Remove(id);

            return RedirectToPage("./Index", new { DeActivated = "True" });

        }

        [PermissionChecker(Roles.DeleteArticle)]
        public IActionResult OnGetActive(int id)
        {
            var result = _articleApplication.Restore(id);

            return RedirectToPage("./Index", new { Activated = "True" });
        }
    }
}
