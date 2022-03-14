﻿using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Delivery.Infrastructure.Models
{
    public class DeliveryAddress : BaseDeletableEntity<string>
    {
        public DeliveryAddress()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [StringLength(36)]
        public override string Id { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public string? DisplayName { get; set; }

        public string? City { get; set; }

        public string? Borough { get; set; }

        public string? Street { get; set; }

        public string? StreetNumber { get; set; }

        public string? Block { get; set; }

        public string? Entry { get; set; }

        public string? Floor { get; set; }

        public string? AddInfo { get; set; }

        [StringLength(36)]
        public string? DeliveryUserId { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{this.City}, кв.{this.Borough}, ул.{this.Street} {this.StreetNumber}, бл.{this.Block}, вх.{this.Entry}, ет.{this.Floor}");
            return sb.ToString();
        }
    }
}
