using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Infrastructure.EFCore.Context;

namespace ShopManagement.Infrastructure.EFCore.Repositories
{
    public class CommentRepository :RepositoryBase<int , Comment>, ICommentRepository
    {
        private readonly ShopContext _context;

        public CommentRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
