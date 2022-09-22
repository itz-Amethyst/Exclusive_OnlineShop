using _0_Framework.Domain;

namespace BlogManagement.Domain.ArticleAgg
{
    public class Article :EntityBase
    {
        public string Title { get; private set; }

        public string ShortDescription { get; private set; }

        public string Description { get; private set; }

        public string Picture { get; private set; }

        public string PictureAlt { get; private set; }

        public string PictureTitle { get; private set; }

        public string Slug { get; private set; }

        public string MetaDescription { get; private set; }

        public string Keywords { get; private set; }

        public string CanonicalAddress { get; private set; }

        public int CategoryId { get; private set; }
        
    }
}
