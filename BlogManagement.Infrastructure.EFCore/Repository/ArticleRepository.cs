using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<int , Article>, IArticleRepository
    {
        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public EditArticle GetDetails(int id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                CanonicalAddress = x.CanonicalAddress,
                CategoryId = x.CategoryId,
                PublishDate = x.PublishDate.ToFarsi(),
                Description = x.Description,
                Image = x.Picture,
                //Picture = x.Picture
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                //Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,

            }).First(x => x.Id == id);
        }

        public Article GetWithCategory(int id)
        {
            return _context.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles.Select(x => new ArticleViewModel()
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Category = x.Category.Name,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                IsDeleted = x.IsDeleted
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
            {
                query = query.Where(x => x.Title.Contains(searchModel.Title));
            }

            if (searchModel.CategoryId > 0)
            {
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            }

            return query.OrderBy(x => x.Id).ToList();
        }
    }
}
