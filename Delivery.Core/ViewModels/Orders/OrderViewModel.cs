namespace Delivery.Core.ViewModels.Orders
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public string? Status { get; set; }

        public string? CreatedOn { get; set; }
    }
}
