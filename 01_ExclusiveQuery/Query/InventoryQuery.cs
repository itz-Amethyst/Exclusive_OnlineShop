using _01_ExclusiveQuery.Contracts.Inventory;
using InventoryManagement.Infrastructure.EFCore.Context;
using ShopManagement.Infrastructure.EFCore.Context;

namespace _01_ExclusiveQuery.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryContext _inventoryContext;

        private readonly ShopContext _shopContext;


        public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }


        public StockStatus CheckStock(IsInStock command)
        {
            var inventory = _inventoryContext.Inventories.FirstOrDefault(x => x.ProductId == command.ProductId);

            if (inventory == null || inventory.CalculateCurrentCount() < command.Count)
            {
                var product = _shopContext.Products.Select(x=> new{x.Id , x.Name}).FirstOrDefault(x => x.Id == command.ProductId);

                return new StockStatus
                {
                    IsInStock = false,
                    ProductName = product.Name
                };
            }

            return new StockStatus
            {
                IsInStock = true
            };
        }
    }
}
