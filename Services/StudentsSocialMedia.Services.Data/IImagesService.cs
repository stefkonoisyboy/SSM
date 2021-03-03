namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Web.ViewModels.Images;

    public interface IImagesService
    {
        IEnumerable<T> GetAllById<T>(string id);

        IEnumerable<T> GetAllLatestById<T>(string id);

        Task Create(CreateImageInputModel input, string path);

        Task Delete(string id);

        Task ChangeProfilePicture(ChangeProfilePictureInputModel input, string path);
    }
}
