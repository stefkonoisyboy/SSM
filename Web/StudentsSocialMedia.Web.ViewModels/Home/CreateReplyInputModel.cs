namespace StudentsSocialMedia.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateReplyInputModel
    {
        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        public string CommentId { get; set; }

        public string AuthorId { get; set; }
    }
}
