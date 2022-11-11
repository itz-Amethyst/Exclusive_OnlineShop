using _0_Framework.Application;
using _01_ExclusiveQuery.Contracts.Order;
using _01_ExclusiveQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore.Context;
using InventoryManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore.Context;

namespace _01_ExclusiveQuery.Query
{
    public class OrderQuery : IOrderQuery
    {
        private readonly ShopContext _shopContext;

        private readonly InventoryContext _inventoryContext;

        private readonly DiscountContext _discountContext;

        public OrderQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<CartQueryModel> GetCartItemsBy(List<CartQueryModel> cartItems)
        {
            List<int> productIds = cartItems.Select(x => x.Id).ToList();

            var inventory = _inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var product = _shopContext.Products.Select(x => new { x.Id, x.Name, x.Picture, x.Slug }).AsNoTracking().ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.IsDeleted == false)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            //var products = _shopContext.Products
            //    .Select(product => new CartQueryModel()
            //    {
            //        Id = product.Id,
            //        Name = product.Name,
            //        Picture = product.Picture,
            //        Slug = product.Slug,
            //        //Count = cartItems.Count,
            //    }).ToList();
            //List<int> products = _shopContext.Products.Where(x => x.Id == productIds).Select(product => new CartQueryModel()
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Picture = product.Picture,
            //    Slug = product.Slug,
            //    //Count = cartItems.Count,
            //}).ToList();


            cartItems.ForEach(cartItem =>
            {
                //cartItems = _shopContext.Products.Select(product => new CartQueryModel
                //{
                //    Id = product.Id,
                //    Name = product.Name,
                //    Picture = product.Picture,
                //    Slug = product.Slug,
                //}).Where(x => x.Id == cartItem.Id).ToList();

                var basketItem = cartItems.FirstOrDefault(x => x.Id == cartItem.Id);


                if (basketItem != null)
                {
                    var inventoryItem = inventory.FirstOrDefault(x => x.ProductId == cartItem.Id);
                    if (inventoryItem != null)
                    {
                        basketItem.UnitPrice = inventoryItem.UnitPrice;
                        basketItem.TotalItemPrice = (basketItem.Count * inventoryItem.UnitPrice);
                        basketItem.Name = product.FirstOrDefault(x => x.Id == cartItem.Id).Name;
                        basketItem.Slug = product.FirstOrDefault(x => x.Id == cartItem.Id).Slug;
                        basketItem.Picture = product.FirstOrDefault(x => x.Id == cartItem.Id).Picture;


                        //basketItem.Count = basketItem.First(x => x.Id == basketItem.Id).Count;
                    }

                    var discountItem = discounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                    if (discountItem != null)
                    {
                        basketItem.HasDiscount = true;
                        basketItem.DiscountRate = discountItem.DiscountRate;
                        var priceWithDiscount = basketItem.PriceWithDiscount = (basketItem.UnitPrice - (inventoryItem.UnitPrice * discountItem.DiscountRate / 100));
                        //var price2 = double.Parse(priceWithDiscount);
                        basketItem.TotalItemPrice = (basketItem.Count * priceWithDiscount);
                    }
                }
            });
            //foreach (var item in products)
            //{

            //    var productInventory = inventory.FirstOrDefault(x => x.ProductId == item.Id);

            //    if (productInventory != null)
            //    {
            //        var price = productInventory.UnitPrice;
            //        item.UnitPrice = price.ToMoney();
            //        var discount = discounts.FirstOrDefault(x => x.ProductId == item.Id);

            //        if (discount != null)
            //        {
            //            int discountRate = discount.DiscountRate;

            //            item.DiscountRate = discountRate;
            //            item.HasDiscount = discountRate > 0;

            //            var discountAmount = Math.Round((price * discountRate) / 100);
            //            item.PriceWithDiscount = (price - discountAmount).ToMoney();
            //        }
            //    }
            //}

            return cartItems;
        }

    }
}
