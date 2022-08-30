using ShopManagement.Application.Contracts.Product;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class CreateInventory
    {
        public int ProductId { get; set; }

        public double UnitPrice { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
