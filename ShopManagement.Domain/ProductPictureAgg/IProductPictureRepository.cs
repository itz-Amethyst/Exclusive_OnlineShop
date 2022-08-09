using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<int , ProductPicture>
    {
        EditProductPicture GetDetails(int id);

        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
