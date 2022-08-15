using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Edit(EditSlide command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Remove(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResult Restore(int id)
        {
            throw new NotImplementedException();
        }

        public EditSlide GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<SlideViewModel> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
