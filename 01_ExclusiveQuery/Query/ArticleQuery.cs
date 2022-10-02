using _01_ExclusiveQuery.Contracts.Article;
using BlogManagement.Infrastructure.EFCore.Context;

namespace _01_ExclusiveQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;

        public ArticleQuery(BlogContext context)
        {
            _context = context;
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            throw new NotImplementedException();
        }
    }
}
