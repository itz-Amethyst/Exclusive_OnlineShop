using _0_Framework.Application;
using _01_ExclusiveQuery.Contracts.Article;
using BlogManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;

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
            return _context.Articles.Include(x=>x.Category).Where(x => x.PublishDate <= DateTime.Now && x.IsDeleted != true)
                .Select(x => new ArticleQueryModel
                {
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryId = x.CategoryId,
                    PublishDate = x.PublishDate.ToFarsi(),
                    IsDeleted = x.IsDeleted,
                    CategoryName = x.Category.Name,
                    CategorySlug = x.Category.Slug
                }).ToList();
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            return _context.Articles.Include(x => x.Category).Where(x => x.PublishDate <= DateTime.Now && x.IsDeleted != true)
                .Select(x => new ArticleQueryModel
                {
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryId = x.CategoryId,
                    PublishDate = x.PublishDate.ToFarsi(),
                    IsDeleted = x.IsDeleted,
                    CategoryName = x.Category.Name,
                    CategorySlug = x.Category.Slug
                }).First(x => x.Slug == slug);
        }
    }
}
