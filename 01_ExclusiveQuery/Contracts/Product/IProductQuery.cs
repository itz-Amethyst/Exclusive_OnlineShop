namespace _01_ExclusiveQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestArrivals();

        List<ProductQueryModel> Search(string value);

        ProductQueryModel GetDetails(string slug);
    }
}
