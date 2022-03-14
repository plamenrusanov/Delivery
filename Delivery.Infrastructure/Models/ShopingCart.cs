using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Models
{
    public class ShopingCart
    {
        public ShopingCart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CartItems = new HashSet<ShopingCartItem>();
        }

        public string DeliveryUserId { get; set; }

        [NotMapped]
        public decimal TotalPrice { get; set; }

        public virtual ICollection<ShopingCartItem> CartItems { get; set; }
    }
}
