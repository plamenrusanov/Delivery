using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class Category : BaseDeletableEntity<string>
    {
        public Category()
        {
            Id = Guid.NewGuid().ToString();
            Products = new List<Product>();
        }

        [Key]
        [StringLength(36)]
        public override string Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        public int Position { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
