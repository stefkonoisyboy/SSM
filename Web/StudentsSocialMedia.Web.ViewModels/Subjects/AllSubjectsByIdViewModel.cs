namespace StudentsSocialMedia.Web.ViewModels.Subjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllSubjectsByIdViewModel : IMapFrom<UserSubject>
    {
        public string SubjectName { get; set; }
    }
}
