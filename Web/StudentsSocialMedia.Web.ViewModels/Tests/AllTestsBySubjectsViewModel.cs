namespace StudentsSocialMedia.Web.ViewModels.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllTestsBySubjectsViewModel : IMapFrom<Test>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SubjectName { get; set; }
    }
}
