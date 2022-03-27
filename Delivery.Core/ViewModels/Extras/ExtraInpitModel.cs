using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Extras
{
    public class ExtraInpitModel
    {
        [MaxLength(30)]
        [Display(Name = "Име")]
        public string? Name { get; set; }

        [Required]
        [Range(0.1, 1000)]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Range(5, 1000)]
        [Display(Name = "Грамаж")]
        public int Weight { get; set; }
    }
}
