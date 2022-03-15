using Delivery.Infrastructure.Common;
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

        [MaxLength(10)]
        public string? Latitude { get; set; }

        [MaxLength(10)]
        public string? Longitude { get; set; }

        public string? DisplayName { get; set; }

        [MaxLength(30)]
        public string? City { get; set; }

        [MaxLength(30)]
        public string? Borough { get; set; }

        [MaxLength(40)]
        public string? Street { get; set; }

        [MaxLength(10)]
        public string? StreetNumber { get; set; }

        [MaxLength(30)]
        public string? Block { get; set; }

        [MaxLength(10)]
        public string? Entry { get; set; }

        [MaxLength(10)]
        public string? Floor { get; set; }

        [MaxLength(100)]
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
