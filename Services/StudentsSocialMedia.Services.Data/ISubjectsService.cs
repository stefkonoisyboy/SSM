namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentsSocialMedia.Web.ViewModels.Subjects;

    public interface ISubjectsService
    {
        IEnumerable<SelectListItem> GetAll();

        IEnumerable<T> GetAllById<T>(string id);

        IEnumerable<string> GetAllNamesById(string id);

        Task Create(CreateSubjectInputModel input);
    }
}
