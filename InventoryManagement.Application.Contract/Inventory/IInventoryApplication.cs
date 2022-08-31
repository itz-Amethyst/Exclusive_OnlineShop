using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);

        OperationResult Edit(EditInventory command);

        OperationResult Increase(IncreaseInventory command);

        //? Customer
        OperationResult Reduce(List<ReduceInventory> command);

        //! Colleague
        OperationResult Reduce(ReduceInventory command);

        EditInventory GetDetails(int id);

        List<InventoryViewModel> Search(InventorySearchModel searchModel);

        List<InventoryOperationViewModel> GetOperationLog(int inventoryId);
    }
}
