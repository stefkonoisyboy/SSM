namespace StudentsSocialMedia.Web.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using StudentsSocialMedia.Web.Infrastructure.ValidationAttributres;

    public class ChangeProfilePictureInputModel
    {
        public string UserId { get; set; }

        [ValidateImagesExtension]
        public IFormFile Image { get; set; }
    }
}
