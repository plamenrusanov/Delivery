using Delivery.Core.ViewModels.ShoppingCart;
using Delivery.Infrastructure.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Orders
{
    public class UserOrderDetailsViewModel
    {
        public UserOrderDetailsViewModel()
        {
            CartItems = new List<ShoppingItemsViewModel>();
        }
        public int OrderId { get; set; }

        public string? CreatedOn { get; set; }

        public string? TimeForDelivery { get; set; }

        public decimal PackagesPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public bool TakeAway { get; set; }

        public List<ShoppingItemsViewModel> CartItems { get; set; }

        public OrderStatus Status { get; set; }

        public int CutleryCount { get; set; }

        [Display(Name = "Коментар: ")]
        public string? CustomerComment { get; set; }
    }
}
