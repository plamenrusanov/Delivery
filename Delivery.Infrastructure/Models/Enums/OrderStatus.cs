using System.ComponentModel.DataAnnotations;

namespace Delivery.Infrastructure.Models.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Необработена")]
        Unprocessed = 1,

        [Display(Name = "Обработва се")]
        Processed = 2,

        [Display(Name = "Пътува")]
        OnDelivery = 3,

        [Display(Name = "Доставена")]
        Delivered = 4,
    }
}
