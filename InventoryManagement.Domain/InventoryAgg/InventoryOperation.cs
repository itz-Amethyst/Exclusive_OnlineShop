namespace InventoryManagement.Domain.InventoryAgg
{
    public class InventoryOperation
    {
        public int Id { get; private set; }

        public bool Operation { get; private set; }

        public int Count { get; private set; }

        public int OperationId { get; private set; }

        public DateTime OperationDate { get; private set; }

        public int CurrentCount { get; private set; }

        public string Description { get; private set; }

        public int OrderId { get; private set; }

        public int InventoryId { get; private set; }

        public Inventory Inventory { get; private set; }

        protected InventoryOperation()
        {

        }

        public InventoryOperation(bool operation, int count, int operationId, int currentCount, string description, int orderId, int inventoryId)
        {
            Operation = operation;
            Count = count;
            OperationId = operationId;
            CurrentCount = currentCount;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;
            OperationDate = DateTime.Now;
        }
    }
}
