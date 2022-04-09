using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Extras
{
    public class ExtraViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string? Name { get; set; }


        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Грамаж")]
        public int Weight { get; set; }
    }
}
