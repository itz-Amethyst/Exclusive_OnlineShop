using _0_Framework.Application;
using _01_ExclusiveQuery.Contracts.Article;
using _01_ExclusiveQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using ServiceHost.Extension;
using ShopManagement.Application.Contracts.Comment;
using System.Net;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;

        public ArticleQueryModel Article;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> ArticleCategories;

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id , bool created = false)
        {
            ViewData["Created"] = created;

            Article = _articleQuery.GetArticleDetails(id);
            LatestArticles = _articleQuery.GetLatestArticles();
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {

            var response = Request.Form["g-recaptcha-response"];
            string secretKey = "6Lf-bm0iAAAAALfFjSkAOJkGmsT8LFS1jYmy6rw4";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");

            if (status)
            {
                command.Type = CommentTypes.Article;
                _commentApplication.Add(command);
                return RedirectToPage("/Article", new { Id = articleSlug, Created = true });
            }

            TempData["Message"] = "Google reCaptcha validation failed";

            return RedirectToPage("/Article");

        }
    }
}
