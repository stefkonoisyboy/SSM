namespace StudentsSocialMedia.Web.ViewModels.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateChoiceInputModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(250)]
        public string Text { get; set; }

        public string IsCorrect { get; set; }
    }
}
