namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Web.ViewModels.Tests;

    public interface IQuestionsService
    {
        Task<string> Create(CreateQuestionInputModel input);

        IEnumerable<T> GetAllById<T>(string id);
    }
}
