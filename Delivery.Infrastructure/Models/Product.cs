using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class Product : BaseDeletableEntity<string>
    {
        public Product()
        {
            Id = Guid.NewGuid().ToString();
            Allergens = new List<AllergensProducts>();
        }

        [Key]
        [StringLength(36)]
        public override string Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        public string? ImageUrl { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public bool HasExtras { get; set; }

        public string? CategoryId { get; set; }

        public Category? Category { get; set; }

        public decimal Price { get; set; }

        public int Weight { get; set; }

        public int MaxProductsInPackage { get; set; }

        public int? PackageId { get; set; }

        public Package? Package { get; set; }

        public ICollection<AllergensProducts> Allergens { get; set; }

    }
}
