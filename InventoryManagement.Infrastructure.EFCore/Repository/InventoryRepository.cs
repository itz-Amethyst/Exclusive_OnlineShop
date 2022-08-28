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
            throw new NotImplementedException();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            throw new NotImplementedException();
        }

        public Inventory GetBy(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
