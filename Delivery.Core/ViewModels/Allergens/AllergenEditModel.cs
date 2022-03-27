using Delivery.Core.ViewModels.CustomValidators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Allergens
{
    public class AllergenEditModel
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Име")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "Изображение")]
        [AllowedImageExtensions(new string[] { ".png" })]
        public IFormFile FormFile { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
