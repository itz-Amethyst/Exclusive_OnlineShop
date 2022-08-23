using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountsAgg;
using DiscountManagement.Infrastructure.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<int , ColleagueDiscount> , IColleagueDiscountRepository
    {
        public readonly DiscountContext _context;

        public ColleagueDiscountRepository(DiscountContext context) : base(context)
        {
            _context = context;
        }

        public EditColleagueDiscount GetDetails(int id)
        {
            return _context.ColleagueDiscounts.Select(x => new EditColleagueDiscount
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId,
            }).First(x => x.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
