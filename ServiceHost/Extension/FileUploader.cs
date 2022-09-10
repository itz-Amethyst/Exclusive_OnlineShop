using _0_Framework.Application;
using Directory = System.IO.Directory;

namespace ServiceHost.Extension
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file ,string path)
        {
            if (file == null)
            {
                return "";
            }
            

            var directoryPath = $"{_webHostEnvironment.WebRootPath}//ProductPictures//{path}";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = $"{directoryPath}{file.FileName}";
            
            using var output = System.IO.File.Create(path);
            file.CopyTo(output);

            return $"{path}/{file.FileName}";
        }
    }
}
