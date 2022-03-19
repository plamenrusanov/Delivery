using Delivery.Core.ViewModels.Allergens;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.AllergensProducts
{
    public class AllergensProductsInputModel
    {
        public AllergensProductsInputModel()
        {
            Allergen = new AllergenViewModel();
        }
        public AllergenViewModel Allergen {get;set;}

        public bool IsCheked { get; set; }
    }
}
