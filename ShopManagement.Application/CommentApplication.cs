using _0_Framework.Application;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operation = new OperationResult();

            var comment = new Comment(command.Name, command.Email, command.Message, command.ProductId);

            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Confirm(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResult Cancel(int id)
        {
            throw new NotImplementedException();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
