using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class Allergen : BaseDeletableEntity<string>
    {
        public Allergen()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new List<AllergensProducts>();
        }

        [Key]
        [StringLength(36)]
        public override string Id { get; set; }

        public string? ImageUrl { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        public virtual ICollection<AllergensProducts> Products { get; set; }
    }
}