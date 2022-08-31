namespace InventoryManagement.Application.Contract.Inventory
{
    public class InventoryOperationViewModel
    {
        public int Id { get; set; }

        public bool Operation { get; set; }

        public int Count { get; set; }

        public int OperationId { get; set; }

        public string Operator { get; set; }

        public DateTime OperationDate { get; set; }

        public int CurrentCount { get; set; }

        public string Description { get; set; }

        public int OrderId { get; set; }

    }
}