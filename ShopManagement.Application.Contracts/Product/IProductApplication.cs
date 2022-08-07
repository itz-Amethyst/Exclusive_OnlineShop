using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);

        OperationResult Edit(EditProduct command);

        EditProduct GetDetails(int id);

        List<ProductViewModel> Search(ProductSearchModel searchModel);

        void InStock(int id);

        void EmptyStock(int id);
    }
}
