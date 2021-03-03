namespace StudentsSocialMedia.Web.ViewModels.Answers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllAnswersByIdViewModel : IMapFrom<Answer>
    {
        public string Text { get; set; }

        public string QuestionId { get; set; }

        public string UserId { get; set; }

        public bool IsCorrect { get; set; }
    }
}
