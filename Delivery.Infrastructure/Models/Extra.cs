using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Infrastructure.Models
{
    public class Extra : BaseDeletableEntity<int>
    {
        public Extra()
        {
            ExtraItems = new List<ExtraItem>();
        }

        [Key]
        public override int Id { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        [Range(5, 1000)]
        public int Weight { get; set; }

        public ICollection<ExtraItem> ExtraItems { get; set; }
    }
}
