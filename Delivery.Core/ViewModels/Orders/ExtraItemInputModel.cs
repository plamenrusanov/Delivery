using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Orders
{
    public class ExtraItemInputModel
    {
        [Required]
        public int id { get; set; }

        [Required]
        [Range(0, 5)]
        public int qty { get; set; }

        [Required]
        [MaxLength(30)]
        public string? name { get; set; }

    }
}
