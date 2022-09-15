using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Comment;

namespace ShopManagement.Domain.CommentAgg
{
    public interface ICommentRepository :IRepository<int , Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
