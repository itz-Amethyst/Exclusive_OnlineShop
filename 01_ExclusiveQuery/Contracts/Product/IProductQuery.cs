using _0_Framework.Application.Cookie;

namespace _01_ExclusiveQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestArrivals();

        List<ProductQueryModel> Search(string value);

        ProductQueryModel GetProductDetails(string slug);

        List<CookieCartModel> CheckInventoryStatus(List<CookieCartModel> cartItems);
    }
}
