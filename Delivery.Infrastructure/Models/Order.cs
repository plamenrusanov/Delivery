﻿using Delivery.Infrastructure.Common;
using Delivery.Infrastructure.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {

            this.Bag = new ShopingCart();
        }

        [Key]
        public override int Id { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? AddInfo { get; set; }

        [StringLength(36)]
        public string? UserId { get; set; }

        public DeliveryUser? DeliveryUser { get; set; }

        public string? AddressId { get; set; }

        public DeliveryAddress? Address { get; set; }

        public string? BagId { get; set; }

        public ShopingCart Bag { get; set; }

        public OrderStatus Status { get; set; }

        public int MinutesForDelivery { get; set; }

        public DateTime? ProcessingTime { get; set; }

        public DateTime? OnDeliveryTime { get; set; }

        public DateTime? DeliveredTime { get; set; }

        public int? DeliveryTaxId { get; set; }

        public DeliveryTax? DeliveryTax { get; set; }

        public string? CustomerComment { get; set; }
    }
}
