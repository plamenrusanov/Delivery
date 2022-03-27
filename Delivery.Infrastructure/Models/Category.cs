using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class Category : BaseDeletableEntity<string>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Category()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Id = Guid.NewGuid().ToString();
            Products = new List<Product>();
        }

        [Key]
        [StringLength(36)]
        public override string Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public int Position { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
