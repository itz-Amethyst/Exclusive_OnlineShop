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
            var operation = new OperationResult();

            var slide = new Slide(command.Picture, command.PictureAlt, command.PictureTitle, command.Title,
                command.Heading, command.BtnText , command.Text);

            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operation.Succeeded();
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
