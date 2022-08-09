using _0_Framework.Domain;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPicture : EntityBase
    {
        public int ProductId { get; private set; }

        public string Picture { get; private set; }

        public string PictureAlt { get; private set; }

        public string PictureTitle { get; private set; }
    }
}
