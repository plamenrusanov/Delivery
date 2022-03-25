using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Packagies
{
    public class PackageInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Дължината на името трябва да е между {1} и {0} символа.")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "1000")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
