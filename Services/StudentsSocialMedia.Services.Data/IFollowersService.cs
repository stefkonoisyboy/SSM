namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Web.ViewModels.Followers;
    using StudentsSocialMedia.Web.ViewModels.SearchFollowers;

    public interface IFollowersService
    {
        IEnumerable<T> GetAllById<T>(string id);

        IEnumerable<T> Get12ById<T>(string id);

        IEnumerable<T> SearchAllByUsername<T>(SearchInputModel input);

        Task Follow(string id, string userToBeFollowedId);

        Task Unfollow(string id, string userToBeUnfollowedId);
    }
}
