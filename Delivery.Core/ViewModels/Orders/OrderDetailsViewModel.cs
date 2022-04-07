using Delivery.Core.ViewModels.ShoppingCart;
using Delivery.Infrastructure.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Orders
{


    public class OrderDetailsViewModel
    {
        public OrderDetailsViewModel()
        {
            CartItems = new List<ShoppingItemsViewModel>();
        }

        public int OrderId { get; set; }

        public string? AddInfo { get; set; }

        public string? UserUserName { get; set; }

        public string? UserPhone { get; set; }

        public string? DisplayAddress { get; set; }

        public string? AddressInfo { get; set; }

        public string? CreatedOn { get; set; }

        public string? TimeForDelivery { get; set; }

        public decimal PackagesPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public List<ShoppingItemsViewModel> CartItems { get; set; }

        public OrderStatus Status { get; set; }

        public int CutleryCount { get; set; }

        [Display(Name = "Коментар: ")]
        public string? CustomerComment { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }
    }
}
