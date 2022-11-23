namespace InventoryManagement.Application.Contract.Inventory
{
    public class ReduceInventory
    {
        public int InventoryId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        public string Description { get; set; }

        public int OrderId { get; set; }

        public ReduceInventory()
        {
            
        }

        public ReduceInventory(int productId, int count, string description, int orderId)
        {
            ProductId = productId;
            Count = count;
            Description = description;
            OrderId = orderId;
        }
    }
}
