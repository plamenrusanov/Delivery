using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.AllergensProducts
{
    public class AllergensProductsInputModel
    {
        [Required]
        public string AllergenId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public bool IsCheked { get; set; }
    }
}
