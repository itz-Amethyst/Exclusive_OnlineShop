using _0_Framework.Domain;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : EntityBase
    {
        public string Name { get; private set; }

        public string Picture { get; private set; }

        public string Description { get; private set; }

        public int ShowOrder { get; private set; }

        //? Seo Props
        public string Slug { get; private set; }

        public string Keywords { get; private set; }

        public string MetaDescription { get; private set; }

        public string CannoicalAddress { get; set; }

        public ArticleCategory(string name, string picture, string description, int showOrder, string slug, string keywords, string metaDescription, string cannoicalAddress)
        {
            Name = name;
            Picture = picture;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CannoicalAddress = cannoicalAddress;
        }
    }
}
