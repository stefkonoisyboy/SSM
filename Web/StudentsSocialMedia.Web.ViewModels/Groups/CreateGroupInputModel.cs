namespace StudentsSocialMedia.Web.ViewModels.Groups
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentsSocialMedia.Web.Infrastructure.ValidationAttributres;

    public class CreateGroupInputModel : BaseGroupInputModel
    {
        [ValidateImagesExtension]
        [Display(Name = "Group Image")]
        public IFormFile Image { get; set; }
    }
}
