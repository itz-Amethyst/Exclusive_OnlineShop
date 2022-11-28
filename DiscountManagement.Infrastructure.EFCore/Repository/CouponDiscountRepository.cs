using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CouponDiscount;
using DiscountManagement.Domain.CouponDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Context;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CouponDiscountRepository : RepositoryBase<int , CouponDiscount> , ICouponDiscountRepository
    {
        private readonly DiscountContext _context;
        public CouponDiscountRepository(DiscountContext context) : base(context)
        {
            _context = context;
        }

        public EditCouponDiscount GetDetails(int id)
        {
            return _context.DiscountsCoupon.Select(x => new EditCouponDiscount()
            {
                Id = x.Id,
                CouponCode = x.DiscountCode,
                UsableCount = x.UsableCount,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToString(),
                EndDate = x.EndDate.ToString(),
                Reason = x.Reason
            }).First(x => x.Id == id);
        }

        public List<CouponDiscountViewModel> Search(CouponDiscountSearchModel searchModel)
        {
            //? Give Me Only Id & Name Better Query

            //var discountOutOfDate = _context.CustomerDiscounts.Select(x => new { x.Id, x.EndDate }).AsNoTracking().ToList();


            var query = _context.DiscountsCoupon.Select(x => new CouponDiscountViewModel()
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                UsableCount = x.UsableCount,
                StartDate = x.StartDate.ToFarsi(),
                StartDateEn = x.StartDate,
                EndDate = x.EndDate.ToFarsi(),
                EndDateEn = x.EndDate,
                Reason = x.Reason,
                CreationDate = x.CreationDate.ToFarsi(),
                IsDeleted = x.IsDeleted,
                IsOutOfDate = x.IsOutOfDate,
            });

            if (!string.IsNullOrWhiteSpace(searchModel.CouponCode))
            {
                query = query.Where(x => x.CouponCode.Contains(searchModel.CouponCode));
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
                    _context.DiscountsCoupon.FirstOrDefault(x => x.Id == item.Id).IsOutOfDate = true;
                }
            }

            var discount = query.OrderByDescending(x => x.Id).ToList();


            discount.ForEach(discount => discount.IsOutOfDate = _context.DiscountsCoupon.FirstOrDefault(x => x.Id == discount.Id).IsOutOfDate);


            //discount = _context.CustomerDiscounts.Select(x => query.Select(x=>x.IsOutOfDate)
            //{
            //    IsOutOfDate = x.IsOutOfDate
            //}).ToList();

            _context.SaveChanges();

            return discount;
        }
    }
}
