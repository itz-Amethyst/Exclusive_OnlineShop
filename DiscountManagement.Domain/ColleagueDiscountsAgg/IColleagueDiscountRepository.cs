using _0_Framework.Domain;
using DiscountManagement.Application.Contract.ColleagueDiscount;

namespace DiscountManagement.Domain.ColleagueDiscountsAgg
{
    public interface IColleagueDiscountRepository : IRepository<int , ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(int id);

        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}
