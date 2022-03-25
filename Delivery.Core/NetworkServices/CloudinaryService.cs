using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Delivery.Core.Contracts;
using Microsoft.AspNetCore.Http;

namespace Delivery.Core.NetworkServices
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Account account;

        public CloudinaryService(string cloudName, string apiKey, string apiSecret)
        {
            this.account = new Account()
            {
                Cloud = cloudName,
                ApiKey = apiKey,
                ApiSecret = apiSecret,
            };
        }

        public async Task<string> UploadImageAsync(IFormFile formFile)
        {
            Stream stream = formFile.OpenReadStream();
            var imageName = Guid.NewGuid().ToString();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageName, stream),
            };

            var uploadResult = await this.Cloudinary().UploadAsync(uploadParams);
            var url = uploadResult.Url.AbsolutePath;
            var index = url.LastIndexOf("/");
            url = url.Substring(index + 1, url.Length - (index + 1));
            return url;
        }

        public async Task DeleteImage(string url)
        {
            var result =  await this.Cloudinary().DestroyAsync(new DeletionParams(url));
        }

        private Cloudinary Cloudinary() => new (this.account);
    }
}
