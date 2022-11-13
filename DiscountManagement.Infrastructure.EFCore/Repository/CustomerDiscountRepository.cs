using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore.Context;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<int, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(int id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToString(),
                EndDate = x.EndDate.ToString(),
                Reason = x.Reason
            }).First(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            //? Give Me Only Id & Name Better Query
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            //var discountOutOfDate = _context.CustomerDiscounts.Select(x => new { x.Id, x.EndDate }).AsNoTracking().ToList();


            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToFarsi(),
                StartDateEn = x.StartDate,
                EndDate = x.EndDate.ToFarsi(),
                EndDateEn = x.EndDate,
                ProductId = x.ProductId,
                Reason = x.Reason,
                CreationDate = x.CreationDate.ToFarsi(),
                IsDeleted = x.IsDeleted,
                IsOutOfDate = x.IsOutOfDate,
            });

            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query.Where(x => x.StartDateEn > searchModel.StartDate.ToGeorgianDateTime());
            }

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                query = query.Where(x => x.EndDateEn < searchModel.EndDate.ToGeorgianDateTime());
            }

            //!:) 

            //query.ForEachAsync(x =>
            //{
            //    var discounts = query.FirstOrDefault(d => d.Id == x.Id);

            //    var endDateEn = query.FirstOrDefault(x => x.Id == discounts.Id).EndDateEn;

            //    int result = DateTime.Compare(endDateEn, DateTime.Now);

            //    if(result < 0)
            //    {
            //        query.FirstOrDefault(x => x.Id == discounts.Id).IsOutOfDate = true;
            //    }

            //});

            foreach (var item in query)
            {
                var endDateEn = item.EndDateEn;

                int result = DateTime.Compare(endDateEn, DateTime.Now);

                if (result < 0)
                {
                    _context.CustomerDiscounts.FirstOrDefault(x => x.Id == item.Id).IsOutOfDate = true;
                }
            }

            var discount = query.OrderByDescending(x => x.Id).ToList();

            discount.ForEach(discount => discount.ProductName = products.First(x => x.Id == discount.ProductId)?.Name);
            
            discount.ForEach(discount => discount.IsOutOfDate = _context.CustomerDiscounts.FirstOrDefault(x => x.Id == discount.Id).IsOutOfDate);


            //discount = _context.CustomerDiscounts.Select(x => query.Select(x=>x.IsOutOfDate)
            //{
            //    IsOutOfDate = x.IsOutOfDate
            //}).ToList();

            _context.SaveChanges();

            return discount;
        }
    }
}
