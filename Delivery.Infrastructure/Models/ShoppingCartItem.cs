using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class ShoppingCartItem : BaseDeletableEntity<int>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ShoppingCartItem()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ExtraItems = new List<ExtraItem>();
        }
        [Key]
        public override int Id { get; set; }
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string ShopingCartId { get; set; }

        public virtual ShoppingCart Cart { get; set; }

        public int Quantity { get; set; }

        public byte Rating { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ExtraItem> ExtraItems { get; set; }
    }
}
