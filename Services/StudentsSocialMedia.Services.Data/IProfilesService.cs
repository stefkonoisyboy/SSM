namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Web.ViewModels.Profiles;

    public interface IProfilesService
    {
        T GetAbout<T>(string id);

        Task UpdateAsync(string id, EditProfileInputModel input);
    }
}
