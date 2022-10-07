namespace CommentManagement.Application.Contract.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public int OwnerRecordId { get; set; }

        public string OwnerName { get; set; }

        public int Type { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsCanceled { get; set; }

        public string CommentCreatedDate { get; set; }
    }
}
