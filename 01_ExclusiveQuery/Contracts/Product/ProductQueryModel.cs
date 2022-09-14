namespace _01_ExclusiveQuery.Contracts.Product
{
    public class ProductQueryModel
    {
        public int Id { get; set; }

        public string Picture { get; set; }

        public string PictureAlt { get; set; }

        public string PictureTitle { get; set; }

        public string Name { get; set; }

        public string? Price { get; set; }

        public string PriceWithDiscount { get; set; }

        public int DiscountRate { get; set; }

        public string ProductCategory { get; set; }

        public string CategorySlug { get; set; }

        public string Slug { get; set; }

        public bool IsDeleted { get; set; }

        public bool HasDiscount { get; set; }

        public DateTime LabelDate { get; set; }

        public bool IsNew { get; set; }

        public string DiscountEndDate { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string Keywords { get; set; }

        public string MetaDescription { get; set; }

        public bool IsInStock { get; set; }
    }
}
