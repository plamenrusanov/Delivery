using Newtonsoft.Json;

namespace Delivery.Core.NetworkServices.Dto
{
    public class AddressDto
    {
        [JsonProperty("address29")]
        public string Address29 { get; set; }

        [JsonProperty("house_number")]
        public string HouseNumber { get; set; }

        [JsonProperty("road")]
        public string Road { get; set; }

        [JsonProperty("neighbourhood")]
        public string Neighbourhood { get; set; }

        [JsonProperty("suburb")]
        public string Suburb { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }
}
