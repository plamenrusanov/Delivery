using Delivery.Core.Contracts;
using Delivery.Core.NetworkServices.Dto;
using Delivery.Core.ViewModels.ShoppingCart;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Delivery.Core.NetworkServices
{
    public class AddresesService : IAddresesService
    {
        private readonly string apiKey;

        public AddresesService(string apiKey)
        {
            this.apiKey = apiKey;
        }
        public async Task<AddressViewModel> GetAddressAsync(string latitude, string longitude)
        {
            if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude))
            {
                throw new ArgumentException();
            }

            try
            {
                HttpClientHandler handler = new ()
                {
                    AutomaticDecompression = DecompressionMethods.GZip,
                };
                using (var client = new HttpClient(handler))
                {
                    var result = await client.GetAsync($"https://eu1.locationiq.com/v1/reverse.php?key={apiKey}&lat={latitude}&lon={longitude}&format=json&accept-language=native");
                    var byteArray = await result.Content.ReadAsByteArrayAsync();
                    var resultContent = ASCIIEncoding.UTF8.GetString(byteArray, 0, byteArray.Length);
                    PositionDto dto = JsonConvert.DeserializeObject<PositionDto>(resultContent);
                    return new AddressViewModel()
                    {
                        DisplayName = dto.DisplayName,
                        Latitude = dto.Latitude,
                        Longitude = dto.Longitude,
                        City = dto.Address.City,
                        Borough = dto.Address.Suburb,
                        Street = dto.Address.Road,
                        StreetNumber = dto.Address.HouseNumber,
                        Block = dto.Address.Address29,
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
