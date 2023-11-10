using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file); // This is the method that will add a photo to Cloudinary
        Task<string> DeletePhoto(string publicId); // This is the method that will delete a photo from Cloudinary
    }
}