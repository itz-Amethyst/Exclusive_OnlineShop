﻿using _0_Framework.Application;
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

            //Todo : need to work
            if (_articleRepository.Exists(x => x.Title == command.Title))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = command.Slug.Slugify();

            var categorySlug = _articleCategoryRepository.GetSlugById(command.CategoryId);

            var path = $"{categorySlug}/{slug}";

            var pictureName = _fileUploader.Upload(command.Picture, path);

            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var article = new Article(command.Title, command.ShortDescription
                , command.Description, pictureName, command.PictureAlt, command.PictureTitle
                , slug, command.MetaDescription, command.Keywords
                , command.CanonicalAddress, command.CategoryId, publishDate);

            _articleRepository.Create(article);
            _articleRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();

            var article = _articleRepository.GetWithCategory(command.Id);

            if (article == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_articleRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug = command.Slug.Slugify();

            var path = $"{article.Category.Slug}/{slug}";

            var pictureName = _fileUploader.Upload(command.Picture, path);

            var publishDate = command.PublishDate.ToGeorgianDateTime();
            article.Edit(command.Title, command.ShortDescription
                , command.Description, pictureName, command.PictureAlt, command.PictureTitle
                , slug, command.MetaDescription, command.Keywords
                , command.CanonicalAddress, command.CategoryId, publishDate);

            _articleRepository.SaveChanges();
            return operation.Succeeded();

        }

        public EditArticle GetDetails(int id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();

            var article = _articleRepository.GetById(id);

            if (article == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            article.Remove();
            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();

            var article = _articleRepository.GetById(id);

            if (article == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            article.Restore();
            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }
    }
}
