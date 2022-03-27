using Delivery.Core.ViewModels.CustomValidators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Allergens
{
    public class AllergenInputModel
    {
        [Required]
        [Display(Name = "Име")]
        [MaxLength(100)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }

        [Required]
        [Display(Name = "Изображение")]
        [AllowedImageExtensions(new string[] { ".png" })]
        public IFormFile Image { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
