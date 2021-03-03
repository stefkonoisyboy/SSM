namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Web.ViewModels.Likes;

    public interface ILikesService
    {
        IEnumerable<T> GetAllById<T>(string id);

        Task Create(CreateLikeInputModel input);

        Task Delete(string postId, string userId);

        int GetCount(string id);
    }
}
