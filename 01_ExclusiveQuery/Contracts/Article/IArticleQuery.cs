namespace _01_ExclusiveQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> GetLatestArticles();
    }
}
