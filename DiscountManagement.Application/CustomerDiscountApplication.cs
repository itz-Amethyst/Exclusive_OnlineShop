using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            if (endDate < startDate)
            {
                return operation.Failed(ApplicationMessages.InputDateNotValid);
            }

            if (startDate == endDate)
            {
                return operation.Failed(ApplicationMessages.DuplicatedDate);
            }
            var customerDiscount = new CustomerDiscount(command.ProductId , command.DiscountRate , startDate , endDate , command.Reason);

            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerDiscount = _customerDiscountRepository.GetById(command.Id);

            if (customerDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            customerDiscount.Edit(command.ProductId, command.DiscountRate, startDate, endDate, command.Reason);

            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();

        }

        public EditCustomerDiscount GetDetails(int id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var customerDiscount = _customerDiscountRepository.GetById(id);
            if (customerDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
            }

            customerDiscount.Remove();
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();
            var customerDiscount = _customerDiscountRepository.GetById(id);
            if (customerDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
            }

            customerDiscount.Restore();
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}