using _0_Framework.Domain;
using DiscountManagement.Application.Contract.CustomerDiscount;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<int, CustomerDiscount>
    {
        EditCustomerDiscount GetDetails(int id);

        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}
