namespace StudentsSocialMedia.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreatePostInputModel
    {
        [Required]
        [MinLength(30)]
        public string Content { get; set; }

        public string SubjectId { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }

        public string GroupId { get; set; }

        public IEnumerable<SelectListItem> GroupsItems { get; set; }

        public string CreatorId { get; set; }
    }
}
