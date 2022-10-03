namespace _01_ExclusiveQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> GetLatestArticles();

        ArticleQueryModel GetArticleDetails(string slug);
    }
}
