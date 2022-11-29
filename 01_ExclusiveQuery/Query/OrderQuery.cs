using _0_Framework.Application;
using _0_Framework.Application.Cookie;
using _01_ExclusiveQuery.Contracts.Order;
using AccountManagement.Infrastructure.EFCore.Context;
using AccountManagement.Infrastructure.EFCore.Security;
using DiscountManagement.Domain.ColleagueDiscountsAgg;
using DiscountManagement.Domain.CouponDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Context;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore.Context;

namespace _01_ExclusiveQuery.Query
{
    public class OrderQuery : IOrderQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly IPermissionChecker _permissionChecker;
        private readonly IAuthHelper _authHelper;
        private readonly AccountContext _accountContext;

        public bool ApplyColleagueDiscount { get; set; }

        public OrderQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, IPermissionChecker permissionChecker, IAuthHelper authHelper, AccountContext accountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _permissionChecker = permissionChecker;
            _authHelper = authHelper;
            _accountContext = accountContext;
        }

        public List<CookieCartModel> GetCartItemsBy(List<CookieCartModel> cartItems)
        {
            //List<int> productIds = cartItems.Select(x => x.Id).ToList();
            var cartSummary = new List<CookieCartModel>();

            var inventory = _inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var product = _shopContext.Products.Select(x => new { x.Id, x.Name, x.Picture, x.Slug }).AsNoTracking().ToList();

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.IsDeleted == false && x.IsOutOfDate == false)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsRemoved)
                .Select(x => new { x.DiscountRate, x.ProductId })
                .ToList();

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

            var username = _authHelper.CurrentAccountUserName();

            if (username != null)
            {
                if (_permissionChecker.CheckUserHasColleagueRole(username))
                {
                    ApplyColleagueDiscount = true;
                }
                else
                {
                    ApplyColleagueDiscount = false;
                }

                //?Zarin Pal

            }

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

                    if (ApplyColleagueDiscount)
                    {
                        var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                        if (colleagueDiscount != null)
                        {
                            basketItem.HasDiscount = true;
                            basketItem.DiscountRate = colleagueDiscount.DiscountRate;
                            basketItem.UnitPriceWithDiscount = (basketItem.UnitPrice - (inventoryItem.UnitPrice * colleagueDiscount.DiscountRate / 100));
                        }
                    }
                    else
                    {
                        var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                        if (customerDiscount != null)
                        {
                            basketItem.HasDiscount = true;
                            basketItem.DiscountRate = customerDiscount.DiscountRate;
                            basketItem.UnitPriceWithDiscount = (basketItem.UnitPrice - (inventoryItem.UnitPrice * customerDiscount.DiscountRate / 100));
                            //var price2 = double.Parse(priceWithDiscount);
                            //basketItem.ItemPayAmount = (basketItem.Count * unitPriceWithDiscount);
                        }
                    }

                    basketItem.DiscountAmount = ((basketItem.TotalItemPrice * basketItem.DiscountRate) / 100);
                    basketItem.ItemPayAmount = basketItem.TotalItemPrice - basketItem.DiscountAmount;

                    cartSummary.Add(cartItem);

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

        public CartModelWithSummary GetSummary(List<CookieCartModel> cartItems)
        {
            var cartSummary = new CartModelWithSummary();

            var username = _authHelper.CurrentAccountUserName();

            if (username != null)
            {
                cartSummary.UserNameForZarinPal = username;
                cartSummary.EmailForZarinPal = _authHelper.CurrentAccountEmail();
            }

            foreach (var cartItem in cartItems)
            {
                cartSummary.Add(cartItem);
            }

            return cartSummary;
        }

        //public DiscountUseType ApplyCouponDiscount(List<CookieCartModel> cartItems, string couponCode)
        //{
        //    var cartSummary = new List<CookieCartModel>();

        //    int userId = _authHelper.CurrentAccountId();

        //    var couponDiscount = _discountContext.DiscountsCoupon
        //        .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now && x.IsDeleted == false && x.IsOutOfDate == false)
        //        .SingleOrDefault(x=>x.DiscountCode == couponCode);

        //    var inventory = _inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice }).ToList();

        //    var product = _shopContext.Products.Select(x => new { x.Id, x.Name, x.Picture, x.Slug }).AsNoTracking().ToList();

        //    if (couponDiscount == null) return DiscountUseType.NotFound;

        //    if (couponDiscount.StartDate != null && couponDiscount.StartDate > DateTime.Now) return DiscountUseType.ExpireDate;

        //    if (couponDiscount.EndDate != null && couponDiscount.EndDate <= DateTime.Now) return DiscountUseType.ExpireDate;

        //    if (couponDiscount.UsableCount != null && couponDiscount.UsableCount < 1) return DiscountUseType.Finished;

        //    if (_discountContext.DiscountUsages.Any(d => d.Id == userId && d.DiscountId == couponDiscount.Id))
        //        return DiscountUseType.UserUsed;

        //    cartItems.ForEach(cartItem =>
        //    {
        //        //cartItems = _shopContext.Products.Select(product => new CartQueryModel
        //        //{
        //        //    Id = product.Id,
        //        //    Name = product.Name,
        //        //    Picture = product.Picture,
        //        //    Slug = product.Slug,
        //        //}).Where(x => x.Id == cartItem.Id).ToList();

        //        var basketItem = cartItems.FirstOrDefault(x => x.Id == cartItem.Id);


        //        if (basketItem != null)
        //        {
        //            var inventoryItem = inventory.FirstOrDefault(x => x.ProductId == cartItem.Id);
        //            if (inventoryItem != null)
        //            {
        //                basketItem.UnitPrice = inventoryItem.UnitPrice;
        //                basketItem.TotalItemPrice = (basketItem.Count * inventoryItem.UnitPrice);
        //                basketItem.Name = product.FirstOrDefault(x => x.Id == cartItem.Id).Name;
        //                basketItem.Slug = product.FirstOrDefault(x => x.Id == cartItem.Id).Slug;
        //                basketItem.Picture = product.FirstOrDefault(x => x.Id == cartItem.Id).Picture;


        //                //basketItem.Count = basketItem.First(x => x.Id == basketItem.Id).Count;
        //            }


        //            basketItem.HasDiscount = true;
        //            basketItem.DiscountRate = couponDiscount.DiscountRate;
        //            basketItem.UnitPriceWithDiscount = (basketItem.UnitPrice - (inventoryItem.UnitPrice * couponDiscount.DiscountRate / 100));


        //            basketItem.DiscountAmount = ((basketItem.TotalItemPrice * basketItem.DiscountRate) / 100);
        //            basketItem.ItemPayAmount = basketItem.TotalItemPrice - basketItem.DiscountAmount;

        //            cartSummary.Add(cartItem);

        //        }
        //    });

        //    if (couponDiscount.UsableCount != null)
        //    {
        //        couponDiscount.UsableCount -= 1;
        //    }

        //    _discountContext.DiscountsCoupon.Update(couponDiscount);
        //    _discountContext.DiscountUsages.Add(new DiscountUsage
        //    {
        //        UserId = userId,
        //        DiscountId = couponDiscount.Id,

        //    });
        //    _discountContext.SaveChanges();

        //    return DiscountUseType.Success;
        //}
    }
}
