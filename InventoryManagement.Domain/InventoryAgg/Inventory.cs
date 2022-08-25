﻿using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public int ProductId { get; private set; }

        public double UnitPrice { get; private set; }

        public bool InStock { get; private set; }

        public List<InventoryOperation> Operations { get; private set; }

        public Inventory(int productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }

        public int CalculateInventoryStock()
        {
            //! Operation 1 == true
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);

            //? Operation 0 == false
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);

            return plus - minus;

        }
    }
}