using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Categories
{
    public class CategoryInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Дължината на името трябва да е между {1} и {0} символа.")]
        [Display(Name = "Име")]
        public string? Name { get; set; }
    }
}
