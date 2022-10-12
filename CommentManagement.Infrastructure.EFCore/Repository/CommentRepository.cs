using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Infrastructure.EFCore.Context;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore.Context;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<int, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;
        private readonly ShopContext _shopContext;
        private readonly BlogContext _blogContext;

        public CommentRepository(CommentContext context, ShopContext shopContext, BlogContext blogContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
            _blogContext = blogContext;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            var articles = _blogContext.Articles.Select(x => new { x.Id, x.Title }).ToList();

            var query = _context.Comments
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    IsConfirmed = x.IsConfirmed,
                    IsCanceled = x.IsCanceled,
                    Message = x.Message,
                    OwnerRecordId = x.OwnerRecordId,
                    Type = x.Type,
                    CommentCreatedDate = x.CreationDate.ToFarsi()
                });

            var comments = query.OrderByDescending(x => x.Id).ToList();

            comments.ForEach(item =>
            {
                item.OwnerNameProduct = products.FirstOrDefault(x => x.Id == item.OwnerRecordId)?.Name;
                item.OwnerNameArticle = articles.FirstOrDefault(x => x.Id == item.OwnerRecordId)?.Title;
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            }

            return comments;
        }
    }
}
