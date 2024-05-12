using CloudinaryDotNet.Actions;

namespace WebApplication1.Data.Services
{
    public interface IPhotosService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
