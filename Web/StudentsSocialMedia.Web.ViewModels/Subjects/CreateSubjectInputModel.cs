namespace StudentsSocialMedia.Web.ViewModels.Subjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateSubjectInputModel
    {
        public string UserId { get; set; }

        public IEnumerable<string> Subjects { get; set; }

        public IEnumerable<SelectListItem> SubjectsItems { get; set; }
    }
}
