namespace StudentsSocialMedia.Web.ViewModels.Groups
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class BaseGroupInputModel
    {
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Name { get; set; }

        [MinLength(10)]
        public string Description { get; set; }

        public string CreatorId { get; set; }

        [Display(Name = "Subject")]
        public string SubjectId { get; set; }

        public IEnumerable<SelectListItem> SubjectsItems { get; set; }
    }
}
