namespace StudentsSocialMedia.Web.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using StudentsSocialMedia.Web.Infrastructure.ValidationAttributres;

    public class CreateImageInputModel
    {
        public string UserId { get; set; }

        [ValidateMultipleImagesExtensions]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
