using InventoryManagement.Application.Contract.Inventory;

namespace _01_ExclusiveQuery.Contracts.Inventory
{
    public interface IInventoryQuery
    {
        StockStatus CheckStock(IsInStock command);
    }
}
