using Microsoft.AspNetCore.Http;

namespace Delivery.Core.ViewModels.Allergens
{
    public class AllergenEditModel
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
