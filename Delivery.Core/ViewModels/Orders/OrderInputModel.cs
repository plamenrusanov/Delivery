using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Orders
{
    public class OrderInputModel
    {
        public OrderInputModel()
        {
            this.Cutlery = this.CreateCutleryList();
            Cart = new List<ShoppingCartItemInputModel>();
        }

        [MaxLength(150, ErrorMessage = "Максимална дължина 150 символа")]
        public string? AddInfoOrder { get; set; }

        public AddressInputModel Address { get; set; } = new ();

        [Required]
        public string Username { get; set; } = String.Empty;

        [Required]
        public string Phone { get; set; } = String.Empty;

        public ICollection<ShoppingCartItemInputModel> Cart { get; set; }

        [Range(0, 5, ErrorMessage = "Приборите може да са между {0} и {1}")]
        public int CutleryCount { get; set; }

        public List<SelectListItem> Cutlery { get; set; }

        private List<SelectListItem> CreateCutleryList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() { Value = "0", Selected = true,  Text = "Не желая прибори" },
                new SelectListItem() { Value = "1", Selected = false,  Text = "1 комплект прибори" },
                new SelectListItem() { Value = "2", Selected = false,  Text = "2 комплекта прибори" },
                new SelectListItem() { Value = "3", Selected = false,  Text = "3 комплекта прибори" },
                new SelectListItem() { Value = "4", Selected = false,  Text = "4 комплекта прибори" },
                new SelectListItem() { Value = "5", Selected = false,  Text = "5 комплекта прибори" },
            };
        }
    }
}
