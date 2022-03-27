using Microsoft.AspNetCore.Http;

namespace Delivery.Core.Contracts
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile formFile);

        Task DeleteImageAsync(string url);
    }
}
