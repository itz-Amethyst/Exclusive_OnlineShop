using _0_Framework.Application;
using _01_ExclusiveQuery.Contracts.Article;
using _01_ExclusiveQuery.Contracts.Comment;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore.Context;
using CommentManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;

namespace _01_ExclusiveQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext context, CommentContext commentContext)
        {
            _context = context;
            _commentContext = commentContext;
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _context.Articles.Include(x=>x.Category).Where(x => x.PublishDate <= DateTime.Now && x.IsDeleted != true)
                .Select(x => new ArticleQueryModel
                {
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    PublishDate = x.PublishDate.ToFarsi(),
                    IsDeleted = x.IsDeleted,
                }).ToList();
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _context.Articles.Include(x => x.Category).Where(x => x.PublishDate <= DateTime.Now && x.IsDeleted != true)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
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
                    PublishDate = x.PublishDate.ToFarsi(),
                    IsDeleted = x.IsDeleted,
                    CategoryName = x.Category.Name,
                    CategorySlug = x.Category.Slug
                }).First(x => x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(article.Keywords))
            {
                article.KeyWordList = article.Keywords.Split(",").ToList();
            }

            article.Comments = _commentContext.Comments
                .Where(x => x.Type == CommentTypes.Product)
                .Where(x => x.OwnerRecordId == article.Id)
                .Where(x => !x.IsCanceled && x.IsConfirmed)
                .Include(x=>x.Parent)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ParentName = x.Parent.Name,
                    ParentId = x.ParentId,
                }).ToList();

            return article;
        }
    }
}
