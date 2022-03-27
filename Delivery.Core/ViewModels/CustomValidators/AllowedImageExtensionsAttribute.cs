using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Core.ViewModels.CustomValidators
{
    public class AllowedImageExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedImageExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            if (value is not IFormFile file)
                return ValidationResult.Success;

            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult("Типът на изображението не е валидно!");
            }

            return ValidationResult.Success;
        }
    }
}
