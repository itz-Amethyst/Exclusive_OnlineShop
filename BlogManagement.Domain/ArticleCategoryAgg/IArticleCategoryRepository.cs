using _0_Framework.Domain;
using BlogManagement.Application.Contracts.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<int , ArticleCategory>
    {
        EditArticleCategory GetDetails(int id);

        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);

        string GetSlugById(int id);
    }
}
