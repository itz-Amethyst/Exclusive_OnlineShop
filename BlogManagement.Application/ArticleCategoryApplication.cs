using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();

            if (_articleCategoryRepository.Exists(x=> x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = command.Slug.Slugify();
            var picturePath =_fileUploader.Upload(command.Picture, slug);

            var articleCategory = new ArticleCategory(command.Name, picturePath, command.Description, command.ShowOrder,
                slug, command.Keywords, command.MetaDescription, command.CannoicalAddress);

            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();

            var articleCategory = _articleCategoryRepository.GetById(command.Id);

            if (articleCategory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_articleCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = command.Slug.Slugify();
            var picturePath = _fileUploader.Upload(command.Picture, slug);

            articleCategory.Edit(command.Name, picturePath, command.Description, command.ShowOrder,
                slug, command.Keywords, command.MetaDescription, command.CannoicalAddress);

            _articleCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }

        public EditArticleCategory GetDetails(int id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }
    }
}