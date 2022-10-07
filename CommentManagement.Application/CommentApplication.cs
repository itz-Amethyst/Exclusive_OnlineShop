using _0_Framework.Application;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;
using ShopManagement.Application.Contracts.Comment;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operation = new OperationResult();

            var comment = new Comment(command.Name ,command.Email , command.Message , command.OwnerRecordId , command.Type , command.ParentId);

            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Confirm(int id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.GetById(id);

            if (comment == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            comment.Confirm();
            _commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Cancel(int id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.GetById(id);

            if (comment == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            comment.Cancel();
            _commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}
