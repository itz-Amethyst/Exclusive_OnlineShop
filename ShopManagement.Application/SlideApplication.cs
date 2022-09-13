using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
        {
            _slideRepository = slideRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();

            if (_slideRepository.Exists(x => x.Title == command.Title && x.Heading == command.Heading))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var pictureName =_fileUploader.Upload(command.Picture, "Slides");

            var slide = new Slide(pictureName, command.PictureAlt, command.PictureTitle, command.Title,
            command.Heading, command.BtnText, command.Text, command.Link);

            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();

            var slide = _slideRepository.GetById(command.Id);

            if (slide == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            var pictureName = _fileUploader.Upload(command.Picture, "Slides");

            slide.Edit(pictureName, command.PictureAlt, command.PictureTitle, command.Title,
            command.Heading, command.BtnText, command.Text, command.Link);

            _slideRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();

            var slide = _slideRepository.GetById(id);

            if (slide == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            slide.Remove();
            _slideRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();

            var slide = _slideRepository.GetById(id);

            if (slide == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            slide.Restore();
            _slideRepository.SaveChanges();

            return operation.Succeeded();
        }

        public EditSlide GetDetails(int id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }
    }
}
