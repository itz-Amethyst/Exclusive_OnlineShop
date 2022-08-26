using _0_Framework.Domain;
using InventoryManagement.Application.Contract.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository <int , Inventory>
    {
        EditInventory GetDetails(int id);

        List<InventoryViewModel> Search(InventorySearchModel searchModel);

        Inventory GetBy(int productId);
    }
}
