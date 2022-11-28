using _0_Framework.Application;
using DiscountManagement.Application.Contract.CouponDiscount;
using DiscountManagement.Domain.CouponDiscountAgg;

namespace DiscountManagement.Application
{
    public class CouponDiscountApplication : ICouponDiscountApplication
    {
        private readonly ICouponDiscountRepository _couponDiscountRepository;

        public CouponDiscountApplication(ICouponDiscountRepository couponDiscountRepository)
        {
            _couponDiscountRepository = couponDiscountRepository;
        }

        public OperationResult Define(DefineCouponDiscount command)
        {
            var operation = new OperationResult();

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            //? need to work
            if (_couponDiscountRepository.Exists(x => x.DiscountCode == command.CouponCode && x.IsOutOfDate == false))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            if (endDate < startDate)
            {
                return operation.Failed(ApplicationMessages.InputDateNotValid);
            }

            if (startDate == endDate)
            {
                return operation.Failed(ApplicationMessages.DuplicatedDate);
            }
            var couponDiscount = new CouponDiscount(command.CouponCode , command.DiscountRate , command.UsableCount , startDate , endDate , command.Reason);

            _couponDiscountRepository.Create(couponDiscount);
            _couponDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCouponDiscount command)
        {
            var operation = new OperationResult();
            var couponDiscount = _couponDiscountRepository.GetById(command.Id);

            if (couponDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_couponDiscountRepository.Exists(x => x.DiscountCode == command.CouponCode && x.IsOutOfDate == false && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            couponDiscount.Edit(command.CouponCode , command.DiscountRate , command.UsableCount , startDate , endDate , command.Reason);

            _couponDiscountRepository.SaveChanges();
            return operation.Succeeded();

        }

        public EditCouponDiscount GetDetails(int id)
        {
            return _couponDiscountRepository.GetDetails(id);
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var couponDiscount = _couponDiscountRepository.GetById(id);
            if (couponDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
            }

            couponDiscount.Remove();
            _couponDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();
            var couponDiscount = _couponDiscountRepository.GetById(id);
            if (couponDiscount == null)
            {
                operation.Failed(ApplicationMessages.RecordNotFound);
            }

            couponDiscount.Restore();
            _couponDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<CouponDiscountViewModel> Search(CouponDiscountSearchModel searchModel)
        {
            return _couponDiscountRepository.Search(searchModel);
        }
    }
}
