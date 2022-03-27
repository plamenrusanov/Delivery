using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.Categories
{
    public class CategoryEditModel : CategoryInputModel
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Id { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int Position { get; set; }
    }
}
