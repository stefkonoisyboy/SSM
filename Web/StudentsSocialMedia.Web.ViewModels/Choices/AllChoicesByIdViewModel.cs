namespace StudentsSocialMedia.Web.ViewModels.Choices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllChoicesByIdViewModel : IMapFrom<Choice>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }
}
