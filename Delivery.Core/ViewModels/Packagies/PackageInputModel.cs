using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Packagies
{
    public class PackageInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Дължината на името трябва да е между {1} и {0} символа.")]
        [Display(Name = "Име")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Required]
        [Range(typeof(decimal), "0.01", "1000")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
