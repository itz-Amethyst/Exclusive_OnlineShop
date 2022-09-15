using _0_Framework.Application;

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
