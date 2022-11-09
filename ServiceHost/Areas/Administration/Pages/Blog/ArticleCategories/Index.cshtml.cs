using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Security;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories
{
    [PermissionChecker(Roles.ManageArticleCategory)]
    public class IndexModel : PageModel
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public ArticleCategorySearchModel SearchModel;
        public List<ArticleCategoryViewModel> ArticleCategories;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _articleCategoryApplication.Search(searchModel);
        }

        [PermissionChecker(Roles.CreateArticleCategory)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategory());
        }

        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        [PermissionChecker(Roles.EditArticleCategory)]
        public IActionResult OnGetEdit(int id)
        {
            var articleCategory = _articleCategoryApplication.GetDetails(id);

            return Partial("Edit", articleCategory);
        }

        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var result = _articleCategoryApplication.Edit(command);

            return new JsonResult(result);
        }
    }
}
