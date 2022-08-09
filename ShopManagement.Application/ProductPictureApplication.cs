using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Remove(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResult Restore(int id)
        {
            throw new NotImplementedException();
        }

        public EditProductPicture GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
