using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

            if (_productPictureRepository.Exists(x => x.Picture == command.Picture.FileName && x.ProductId == command.ProductId))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var product = _productRepository.GetProductWithCategory(command.ProductId);

            var path = $"{product.Category.Slug}/{product.Slug}";

            var picturePath = _fileUploader.UploadProductPicture(command.Picture, path);

            var productPicture = new ProductPicture(command.ProductId, picturePath, command.PictureAlt,
                command.PictureTitle);

            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.GetWithProductAndCategory(command.Id);

            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            var path = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";

            var picturePath = _fileUploader.UploadProductPicture(command.Picture, path);

            if (_productPictureRepository.Exists(x =>
                    x.Picture == picturePath && x.ProductId == command.ProductId && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            productPicture.Edit(command.ProductId , picturePath , command.PictureAlt , command.PictureTitle);
            _productPictureRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.GetById(id);

            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();

            var productPicture = _productPictureRepository.GetById(id);

            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProductPicture GetDetails(int id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
