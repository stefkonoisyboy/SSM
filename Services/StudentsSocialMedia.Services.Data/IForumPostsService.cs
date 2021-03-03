namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Web.ViewModels.Forum.Posts;

    public interface IForumPostsService
    {
        Task Create(CreatePostInputModel input);

        IEnumerable<T> GetAllById<T>(string id);
    }
}
