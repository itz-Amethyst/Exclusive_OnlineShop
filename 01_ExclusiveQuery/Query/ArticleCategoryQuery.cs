using _0_Framework.Application;
using _01_ExclusiveQuery.Contracts.Article;
using _01_ExclusiveQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace _01_ExclusiveQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    Slug = x.Slug,
                    ArticlesCount = x.Articles.Count,
                }).ToList();
        }

        public ArticleCategoryQueryModel GetArticleCategoryBySlug(string slug)
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryQueryModel
            {
                Slug = x.Slug,
                Name = x.Name,
                Description = x.Description,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress,
                Articles = MapArticles(x.Articles)
            }).First(x=> x.Slug == slug);
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> xArticles)
        {
            return xArticles.Select(x => new ArticleQueryModel
            {
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Title = x.Title,
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                PublishDate = x.PublishDate.ToFarsi(),
            }).ToList();
        }
    }
}
