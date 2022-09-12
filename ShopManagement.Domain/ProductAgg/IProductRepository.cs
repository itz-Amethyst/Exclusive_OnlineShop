using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<int , Product>
    {
        EditProduct GetDetails(int id);

        List<ProductViewModel> GetProducts();

        List<ProductViewModel> Search(ProductSearchModel searchModel);

        Product GetProductWithCategory(int id);
    }
}
