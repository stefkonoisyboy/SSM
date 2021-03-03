namespace StudentsSocialMedia.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateCommentInputModel
    {
        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        public string PostId { get; set; }

        public string AuthorId { get; set; }
    }
}
