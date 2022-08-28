using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore.Context;
using ShopManagement.Infrastructure.EFCore.Context;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<int , Inventory> , IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditInventory GetDetails(int id)
        {
            return _context.Inventories.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).First(x => x.Id == id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.Inventories.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                InStock = x.InStock,
                ProductId = x.ProductId,
                CurrentCount = x.CalculateCurrentCount()
            });

            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (searchModel.InStock)
            {
                //!Includes Not In Stock
                query = query.Where(x => !x.InStock);
            }

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            inventory.ForEach(item =>
            {
                item.ProductName = products.First(x => x.Id == item.ProductId)?.Name;
            });

            return inventory;
        }

        public Inventory GetBy(int productId)
        {
            return _context.Inventories.First(x => x.ProductId == productId);
        }
    }
}
