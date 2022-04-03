namespace Delivery.Core.ViewModels.Orders
{
    public class AddressInputModel
    {
        public string? latitude { get; set; }
        public string? longitude { get; set; }
        public string? displayName { get; set; }
        public string? city { get; set; }
        public string? borough { get; set; }
        public string? street { get; set; }
        public string? streetNumber { get; set; }
        public string? block { get; set; }
        public string? entry { get; set; }
        public string? floor { get; set; }
        public string? addInfo { get; set; }
    }
}
