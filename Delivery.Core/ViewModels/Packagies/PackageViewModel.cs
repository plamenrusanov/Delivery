using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Packagies
{
    public class PackageViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

    }
}
