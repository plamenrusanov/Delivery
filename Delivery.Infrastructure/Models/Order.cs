using Delivery.Infrastructure.Common;
using Delivery.Infrastructure.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {

            CartItems = new List<ShoppingCartItem>();
        }

        [Key]
        public override int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string AddInfo { get; set; }

        [StringLength(36)]
        public string DeliveryUserId { get; set; }

        public virtual DeliveryUser DeliveryUser { get; set; }

        [StringLength(36)]
        public string AddressId { get; set; }

        public virtual DeliveryAddress Address { get; set; }

        public OrderStatus Status { get; set; }

        public int MinutesForDelivery { get; set; }

        public DateTime? ProcessingTime { get; set; }

        public DateTime? OnDeliveryTime { get; set; }

        public DateTime? DeliveredTime { get; set; }

        [MaxLength(200)]
        public string CustomerComment { get; set; }

        public virtual ICollection<ShoppingCartItem> CartItems { get; set; }
    }
}
