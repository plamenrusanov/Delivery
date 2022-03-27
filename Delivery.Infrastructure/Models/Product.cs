using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Infrastructure.Models
{
    public class Product : BaseDeletableEntity<string>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Product()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Id = Guid.NewGuid().ToString();
            Allergens = new List<AllergensProducts>();
        }

        [Key]
        [StringLength(36)]
        public override string Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } 

        [MaxLength(100)]
        public string ImageUrl { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public bool HasExtras { get; set; }

        [StringLength(36)]
        public string CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal Price { get; set; }

        [Range(1, 1000)]
        public int Weight { get; set; }

        [Range(1, 10)]
        public int MaxProductsInPackage { get; set; }

        public int PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public virtual Package Package { get; set; }

        public virtual ICollection<AllergensProducts> Allergens { get; set; }

    }
}
