using Delivery.Core.ViewModels.Allergens;
using Delivery.Core.ViewModels.AllergensProducts;
using Delivery.Core.ViewModels.Categories;
using Delivery.Core.ViewModels.Packagies;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Products
{
    public class ProductInputModel
    {
        public ProductInputModel()
        {
                Categories = new List<CategoryViewModel>();
            Packages = new List<PackageViewModel>();
            Allergens = new List<AllergensProductsInputModel>();
        }

        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Снимка")]
        public IFormFile Image { get; set; }

        [MaxLength(100)]
        public string ImageUrl { get; set; }

        [MaxLength(200)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Ще има ли екстри")]
        public bool HasExtras { get; set; }

        [Required]
        [StringLength(36)]
        [Display(Name = "Категория")]
        public string CategoryId { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }

        [Required]
        [Range(0.1, 1000)]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Range(1, 1000)]
        [Display(Name = "Грамаж")]
        public int Weight { get; set; }

        [Range(1, 10)]
        [Display(Name = "Максимални бройки в опаковка")]
        public int MaxProductsInPackage { get; set; }

        [Display(Name = "Опаковка")]
        public int PackageId { get; set; }

        public ICollection<PackageViewModel> Packages { get; set; }

        [Display(Name = "Алергени")]
        public List<AllergensProductsInputModel> Allergens { get; set; }

        //public ICollection<AllergenViewModel> AvailableAllergens { get; set; }

    }
}
