﻿namespace _01_ExclusiveQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        List<ArticleCategoryQueryModel> GetArticleCategories();

        ArticleCategoryQueryModel GetArticleCategoryBySlug(string slug);
    }
}
