using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class AllergensProducts
    {
        [StringLength(36)]
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }
         
        [StringLength(36)]
        public string AllergenId { get; set; }

        public virtual Allergen Allergen { get; set; }
    }
}