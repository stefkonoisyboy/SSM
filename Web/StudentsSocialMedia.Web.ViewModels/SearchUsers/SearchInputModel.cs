namespace StudentsSocialMedia.Web.ViewModels.SearchUsers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class SearchInputModel
    {
        [Required]
        public string UserName { get; set; }
    }
}
