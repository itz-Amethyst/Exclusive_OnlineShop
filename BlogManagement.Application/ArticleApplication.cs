using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticle command)
        {
            throw new NotImplementedException();
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
