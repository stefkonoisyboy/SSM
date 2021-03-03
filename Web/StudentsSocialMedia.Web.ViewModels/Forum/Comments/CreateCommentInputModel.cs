namespace StudentsSocialMedia.Web.ViewModels.Forum.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateCommentInputModel
    {
        [Required]
        [MinLength(30)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public string PostId { get; set; }
    }
}
