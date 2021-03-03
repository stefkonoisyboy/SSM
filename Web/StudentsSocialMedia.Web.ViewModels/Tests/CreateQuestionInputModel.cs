namespace StudentsSocialMedia.Web.ViewModels.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateQuestionInputModel
    {
        [Required]
        [MinLength(10)]
        [MaxLength(250)]
        public string Text { get; set; }

        public string TestId { get; set; }

        public IEnumerable<SelectListItem> TestsItems { get; set; }

        public IEnumerable<CreateChoiceInputModel> Choices { get; set; }
    }
}
