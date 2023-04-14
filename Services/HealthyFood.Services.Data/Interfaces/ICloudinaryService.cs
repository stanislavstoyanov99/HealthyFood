namespace HealthyFood.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadAsync(IFormFile file, string fileName);

        Task DeleteImage(Cloudinary cloudinary, string name);
    }
}
