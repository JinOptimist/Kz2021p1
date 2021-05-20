using System;
using System.Threading.Tasks;
using WebApplication1.ViewModels;

namespace WebApplication1.Services
{
    public interface IBlobService
    {
        Task<Uri> UploadPhotoAsync(FileUpload fileUpload);
        Task<Uri> GetPhotoAsync(string photoName);
    }
}