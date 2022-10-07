using _0_Framework.Application;
using CommentManagement.Application.Contract.Comment;

namespace ShopManagement.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment command);

        OperationResult Confirm(int id);

        OperationResult Cancel(int id);

        List<CommentViewModel> Search(CommentSearchModel searchModel);

    }
}
