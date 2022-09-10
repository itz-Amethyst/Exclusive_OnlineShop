using _0_Framework.Application;

namespace ServiceHost.Extension
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file)
        {
            if (file == null)
            {
                return "";
            }

            var path = $"{_webHostEnvironment.WebRootPath}//ProductPictures//{file.FileName}";

            using var output = System.IO.File.Create(path);
            file.CopyToAsync(output);

            return file.FileName;
        }
    }
}
