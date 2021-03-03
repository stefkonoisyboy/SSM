namespace StudentsSocialMedia.Web.Infrastructure.ValidationAttributres
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class ValidateMultipleImagesExtensionsAttribute : ValidationAttribute
    {
        public ValidateMultipleImagesExtensionsAttribute()
        {
            this.ErrorMessage = "Invalid file format!";
        }

        public override bool IsValid(object value)
        {
            ICollection<string> allowedExtensions = new List<string>() { ".jpg", ".png", ".gif" };

            if (value is IEnumerable<IFormFile> imageValue)
            {
                foreach (var image in imageValue)
                {
                    string extension = Path.GetExtension(image.FileName);
                    if (!allowedExtensions.Contains(extension))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
