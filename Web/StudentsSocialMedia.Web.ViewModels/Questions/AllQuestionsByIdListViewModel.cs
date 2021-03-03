namespace StudentsSocialMedia.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllQuestionsByIdListViewModel
    {
        public IEnumerable<AllQuestionsByIdViewModel> Questions { get; set; }
    }
}
