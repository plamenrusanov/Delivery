using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Models
{
    public class ShopingCartItem
    {
        public string? ProductId { get; set; }

        public Product? Product { get; set; }

        public string? ShopingCartId { get; set; }

        public ShopingCart? Cart { get; set; }

        public int Quantity { get; set; }

        public byte Rating { get; set; }

        public string? Description { get; set; }

        public ICollection<ExtraItem>? ExtraItems { get; set; }
    }
}
