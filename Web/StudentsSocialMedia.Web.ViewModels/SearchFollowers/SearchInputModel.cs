namespace StudentsSocialMedia.Web.ViewModels.SearchFollowers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class SearchInputModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string UserName { get; set; }

        public string UserId { get; set; }
    }
}
