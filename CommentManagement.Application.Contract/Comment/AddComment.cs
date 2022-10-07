namespace CommentManagement.Application.Contract.Comment
{
    public class AddComment
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public int ProductId { get; set; }
        
    }
}
