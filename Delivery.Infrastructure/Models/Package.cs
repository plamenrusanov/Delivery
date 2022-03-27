using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Infrastructure.Models
{
    public class Package : BaseDeletableEntity<int>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Package()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Products = new List<Product>();
        }

        [Key]
        public override int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal Price { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}