using _0_Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        public double UnitPrice { get; private set; }

        public bool IsInStock { get; private set; }

        public string ShortDescription { get; private set; }

        public string Description { get; private set; }

        public string Picture { get; private set; }

        public string PictureAlt { get; private set; }

        public string PictureTitle { get; private set; }

        public int CategoryId { get; private set; }

        public string Slug { get; private set; }

        public string Keywords { get; private set; }

        public string MetaDescription { get; private set; }

        public ProductCategory Category { get; private set; }

    }
}
