using Newtonsoft.Json;

namespace Delivery.Core.ViewModels.Rating
{

    public class RatingItemDto
    {
        [JsonProperty(propertyName: "itemId")]
        public string? ItemId { get; set; }

        [JsonProperty(propertyName: "rating")]
        public string? Rating { get; set; }
    }
}
