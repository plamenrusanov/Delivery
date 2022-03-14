using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Infrastructure.Models
{
    public class DeliveryTax : BaseDeletableEntity<int>
    {
        [Key]
        public override int Id { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }
    }
}