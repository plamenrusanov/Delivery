using Delivery.Core.ViewModels.Categories;
using Delivery.Core.ViewModels.Packagies;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Products
{
    public class ProductInputModel
    {

        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string ImageUrl { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public bool HasExtras { get; set; }

        [Required]
        [StringLength(36)]
        public string CategoryId { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }

        [Required]
        [Range(0.1, 1000)]
        public decimal Price { get; set; }

        [Range(1, 1000)]
        public int Weight { get; set; }

        [Range(1, 10)]
        public int MaxProductsInPackage { get; set; }

        public int PackageId { get; set; }

        public ICollection<PackageViewModel> Packages { get; set; }

        //public ICollection<AllergensProducts> Allergens { get; set; }

    }
}
