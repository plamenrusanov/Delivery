using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class Allergen : BaseDeletableEntity<string>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Allergen()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new List<AllergensProducts>();
        }

        [Key]
        [StringLength(36)]
        public override string Id { get; set; }

        [MaxLength(100)]
        public string ImageUrl { get; set; }


        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<AllergensProducts> Products { get; set; }
    }
}