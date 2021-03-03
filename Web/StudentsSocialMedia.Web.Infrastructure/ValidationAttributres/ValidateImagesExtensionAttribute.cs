namespace StudentsSocialMedia.Web.Infrastructure.ValidationAttributres
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class ValidateImagesExtensionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            ICollection<string> allowedExtensions = new List<string>() { ".jpg", ".png", ".gif" };

            if (value is IFormFile imageValue)
            {
                string extension = Path.GetExtension(imageValue.FileName);
                if (!allowedExtensions.Contains(extension))
                {
                    return false;
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
