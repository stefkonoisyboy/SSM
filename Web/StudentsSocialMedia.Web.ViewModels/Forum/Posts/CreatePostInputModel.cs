namespace StudentsSocialMedia.Web.ViewModels.Forum.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreatePostInputModel
    {
        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        public string Content { get; set; }

        public string UserId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public IEnumerable<SelectListItem> CategoryItems { get; set; }
    }
}
