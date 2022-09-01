namespace _01_ExclusiveQuery.Contracts.ProductCategory
{
    public class ProductCategoryQueryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //!Note For better seo Quality can put description in Description Component and add hide class into it
        public string Description { get; set; }

        public string Picture { get; set; }

        public string PictureAlt { get; set; }

        public string PictureTitle { get; set; }

        public string Slug { get; set; }
    }
}
