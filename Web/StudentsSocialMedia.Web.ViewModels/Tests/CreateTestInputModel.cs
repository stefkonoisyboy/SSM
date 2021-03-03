namespace StudentsSocialMedia.Web.ViewModels.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateTestInputModel
    {
        public string Name { get; set; }

        public string SubjectId { get; set; }

        public IEnumerable<SelectListItem> SubjectsItems { get; set; }

        public string CreatorId { get; set; }
    }
}
