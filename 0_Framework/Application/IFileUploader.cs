using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public interface IFileUploader
    {
        string Upload(IFormFile file ,string path);

        string UploadProductPicture(IFormFile file, string path);

    }
}
