﻿using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore.Context;
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
        private readonly AccountContext _accountContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext, AccountContext accountContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
            _accountContext = accountContext;
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
                CurrentCount = x.CalculateCurrentCount(),
                CreationDate = x.CreationDate.ToFarsi()
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

        public List<InventoryOperationViewModel> GetOperationLog(int inventoryId)
        {
            var account = _accountContext.Accounts.Select(x => new { x.Id, x.Username }).ToList();

            var inventory = _context.Inventories.First(x => x.Id == inventoryId);

            var operations = inventory.Operations.Select(x => new InventoryOperationViewModel
            {
                Id = x.Id,
                Count = x.Count,
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                Operation = x.Operation,
                OperationDate = x.OperationDate.ToFarsi(),
                OperationId = x.OperationId,
                OrderId = x.OrderId
            }).OrderByDescending(x=> x.Id).ToList();

            foreach (var operation in operations)
            {
                operation.Operator = account.FirstOrDefault(x => x.Id == operation.OperationId)?.Username;
            }

            return operations;
        }
    }
}
