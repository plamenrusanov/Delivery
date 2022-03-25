using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Categories
{
    public class CategoryEditModel : CategoryInputModel
    {
        public string Id { get; set; }

        public int Position { get; set; }
    }
}
