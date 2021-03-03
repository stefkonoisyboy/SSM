namespace StudentsSocialMedia.Web.ViewModels.SearchGroups
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SearchInputModel
    {
        public string Name { get; set; }

        public string SubjectId { get; set; }

        public IEnumerable<SelectListItem> SubjectsItems { get; set; }
    }
}
