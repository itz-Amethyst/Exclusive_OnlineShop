namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        public int ProductId { get; set; }

        public string Picture { get; set; }

        public string PictureAlt { get; set; }

        public string PictureTitle { get; set; }
    }
}
