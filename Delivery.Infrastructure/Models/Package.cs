using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Infrastructure.Models
{
    public class Package : BaseDeletableEntity<int>
    {
        [Key]
        public override int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal Price { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}