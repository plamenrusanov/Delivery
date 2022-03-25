using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Packagies
{
    public class PackageViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

    }
}
