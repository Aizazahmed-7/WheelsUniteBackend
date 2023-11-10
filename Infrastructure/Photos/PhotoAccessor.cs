using Application.Interfaces;
using Application.Photos;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {


        private readonly Cloudinary cloudinary;
        public PhotoAccessor(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            cloudinary = new Cloudinary(account);

        }
        public async Task<PhotoUploadResult> AddPhoto(IFormFile file)
        {
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams // This is the upload parameters for the photo
                {
                    File = new FileDescription(file.FileName, stream), // This is the file name and the stream of the file
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill") // This is the transformation that will be applied to the photo
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams); // This is the result of the upload
                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }
                return new PhotoUploadResult
                {
                    PublicId = uploadResult.PublicId, // This is the public ID of the photo
                    Url = uploadResult.SecureUrl.ToString() // This is the URL of the photo
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<string> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId); // This is the deletion parameters for the photo
            var result = await cloudinary.DestroyAsync(deleteParams); // This is the result of the deletion
            return result.Result == "ok" ? result.Result : null;
        }
    }
}