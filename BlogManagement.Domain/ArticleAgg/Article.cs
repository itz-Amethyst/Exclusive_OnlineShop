using _0_Framework.Domain;
using BlogManagement.Domain.ArticleCategoryAgg;

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

        public ArticleCategory Category { get; private set; }

        public DateTime PublishDate { get; private set; }

        public bool IsDeleted { get; private set; }

        public Article(string title, string shortDescription, string description, string picture, string pictureAlt, string pictureTitle, string slug, string metaDescription, string keywords, string canonicalAddress, int categoryId, DateTime publishDate)
        {
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            MetaDescription = metaDescription;
            Keywords = keywords;
            CanonicalAddress = canonicalAddress;
            CategoryId = categoryId;
            PublishDate = publishDate;
            IsDeleted = false;
        }

        public void Edit(string title, string shortDescription, string description, string picture, string pictureAlt, string pictureTitle, string slug, string metaDescription, string keywords, string canonicalAddress, int categoryId, DateTime publishDate)
        {
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            MetaDescription = metaDescription;
            Keywords = keywords;
            CanonicalAddress = canonicalAddress;
            CategoryId = categoryId;
            PublishDate = publishDate;
        }

        public void Remove()
        {
            IsDeleted = true;
        }

        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
