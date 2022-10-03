using _01_ExclusiveQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;

        public ArticleQueryModel Article;

        public ArticleModel(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public void OnGet(string slug)
        {
            Article = _articleQuery.GetArticleDetails(slug);
        }
    }
}
