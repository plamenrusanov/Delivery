using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Allergens
{
    public class AllergenInputModel
    {
        [Required]
        [Display(Name = "Име")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Изображение")]
        public IFormFile Image { get; set; }
    }
}
