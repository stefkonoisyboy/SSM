namespace StudentsSocialMedia.Web.ViewModels.Hobbies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateHobbyInputModel
    {
        public string UserId { get; set; }

        public IEnumerable<string> Hobbies { get; set; }

        public IEnumerable<SelectListItem> HobbiesItems { get; set; }
    }
}
