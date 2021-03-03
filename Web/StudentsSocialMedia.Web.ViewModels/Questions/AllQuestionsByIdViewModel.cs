namespace StudentsSocialMedia.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Answers;
    using StudentsSocialMedia.Web.ViewModels.Choices;

    public class AllQuestionsByIdViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public IEnumerable<AllChoicesByIdViewModel> Choices { get; set; }

        public IEnumerable<AllAnswersByIdViewModel> Answers { get; set; }
    }
}
