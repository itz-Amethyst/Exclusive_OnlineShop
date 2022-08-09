using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);

        OperationResult Edit(EditProductPicture command);

        OperationResult Remove(int id);

        OperationResult Restore(int id);

        EditProductPicture GetDetails(int id);

        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
