using _0_Framework.Domain;
using BlogManagement.Application.Contracts.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<int , Article>
    {
        EditArticle GetDetails(int id);

        Article GetWithCategory(int id);

        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
