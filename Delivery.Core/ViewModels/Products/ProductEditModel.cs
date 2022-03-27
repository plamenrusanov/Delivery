using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Products
{
    public class ProductEditModel :ProductInputModel
    {
        public ProductEditModel() : base()
        {}

        [Required]
        public string Id { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Снимка")]
        public new IFormFile? Image { get; set; }

        public string ImageUrl { get; set; }
    }
}
