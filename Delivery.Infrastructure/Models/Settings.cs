using Delivery.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models
{
    public class Settings : BaseEntity<int>
    {
        [Key]
        public override int Id { get; set; }

        public string? Key { get; set; }

        public string? Value { get; set; }
    }
}
