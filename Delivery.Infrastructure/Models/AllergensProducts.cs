using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Infrastructure.Models
{
    public class AllergensProducts : IDeletableEntity
    {
        [StringLength(36)]
        public string? ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product{ get; set; }

        [StringLength(36)]
        public string? AllergenId { get; set; }

        [ForeignKey(nameof(AllergenId))]
        public Allergen? Allergen { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}