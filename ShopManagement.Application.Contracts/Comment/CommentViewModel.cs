namespace ShopManagement.Application.Contracts.Comment
{
    public class CommentViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsCanceled { get; set; }
    }
}
