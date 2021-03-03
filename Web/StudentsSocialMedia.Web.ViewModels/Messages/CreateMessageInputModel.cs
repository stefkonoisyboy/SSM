namespace StudentsSocialMedia.Web.ViewModels.Messages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateMessageInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(500)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public string GroupId { get; set; }
    }
}
