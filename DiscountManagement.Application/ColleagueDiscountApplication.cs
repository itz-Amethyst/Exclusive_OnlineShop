using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountsAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_colleagueDiscountRepository.Exists(x =>
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(colleagueDiscount);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetById(command.Id);
            if (colleagueDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_colleagueDiscountRepository.Exists(x =>
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            colleagueDiscount.Edit(command.ProductId,command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetById(id);
            if (colleagueDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
            }

            colleagueDiscount.Remove();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(int id)
        {
            throw new NotImplementedException();
        }

        public EditColleagueDiscount GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
