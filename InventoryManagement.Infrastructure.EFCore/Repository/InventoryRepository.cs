using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<int , Inventory> , IInventoryRepository
    {
        private readonly InventoryContext _context;

        public InventoryRepository(InventoryContext context) : base(context)
        {
            _context = context;
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
            throw new NotImplementedException();
        }

        public Inventory GetBy(int productId)
        {
            return _context.Inventories.First(x => x.ProductId == productId);
        }
    }
}
