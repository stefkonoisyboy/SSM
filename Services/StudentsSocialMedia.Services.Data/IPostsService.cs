namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Web.ViewModels.Home;
    using StudentsSocialMedia.Web.ViewModels.Posts;

    public interface IPostsService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllById<T>(string id);

        IEnumerable<T> GetAllWithGroup<T>();

        IEnumerable<T> GetAllByGroupId<T>(string id);

        Task CreateAsync(CreatePostInputModel input);
    }
}
