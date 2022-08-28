using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();

            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var inventory = new Inventory(command.ProductId, command.UnitPrice);

            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetById(command.Id);

            if (inventory == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            inventory.Edit(command.ProductId , command.UnitPrice);

            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            throw new NotImplementedException();
        }

        public EditInventory GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}