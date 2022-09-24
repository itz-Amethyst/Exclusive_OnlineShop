using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();

            if (_articleRepository.Exists(x => x.Title == command.Title))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = command.Slug.Slugify();

            var categorySlug = _articleCategoryRepository.GetSlugById(command.CategoryId);

            var path = $"{categorySlug}/{slug}";

            var pictureName =_fileUploader.Upload(command.Picture, path);

            var article = new Article(command.Title, command.ShortDescription
                , command.Description, command.PictureAlt, command.PictureTitle
                , pictureName, command.Keywords, command.MetaDescription
                , slug, command.CanonicalAddress , command.CategoryId , DateTime.Now);

            _articleRepository.Create(article);
            _articleRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            throw new NotImplementedException();
        }

        public EditArticle GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
