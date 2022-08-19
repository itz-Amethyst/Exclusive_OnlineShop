using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore.Context;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<int , CustomerDiscount>, ICustomerDiscountRepository
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
            }).First(x=>x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            //? Give Me Only Id & Name Better Query
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToString(),
                StartDateEn = x.StartDate,
                EndDate = x.EndDate.ToString(),
                EndDateEn = x.EndDate,
                ProductId = x.ProductId,
                Reason = x.Reason
            });

            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                var startDate = DateTime.Now;
                query = query.Where(x => x.StartDateEn > startDate);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                var endDate = DateTime.Now;
                query = query.Where(x => x.EndDateEn < endDate);
            }

            var discount = query.OrderByDescending(x => x.Id).ToList();

            discount
                .ForEach(discount => discount.ProductName = products.First(x=>x.Id == discount.ProductId)?.Name);

            return discount;
        }
    }
}
