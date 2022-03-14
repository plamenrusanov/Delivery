using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Infrastructure.Models
{
    public class Extra : BaseDeletableEntity<int>
    {
        [Key]
        public override int Id { get; set; }

        public string? Name { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        public int Weight { get; set; }

        public ICollection<ExtraItem>? ExtraItems { get; set; }
    }
}
