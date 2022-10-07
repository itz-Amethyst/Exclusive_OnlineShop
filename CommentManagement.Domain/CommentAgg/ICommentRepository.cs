using _0_Framework.Domain;
using CommentManagement.Application.Contract.Comment;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository :IRepository<int , Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
