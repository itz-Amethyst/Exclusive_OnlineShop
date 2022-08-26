using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.EFCore.Context
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {

        }
    }
}