namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Web.ViewModels.Profiles;
    using StudentsSocialMedia.Web.ViewModels.SearchUsers;

    public interface IUsersService
    {
        T GetById<T>(string id);

        IEnumerable<T> GetAllByName<T>(SearchInputModel input, string id);
    }
}
