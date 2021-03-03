namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentsSocialMedia.Web.ViewModels.Groups;
    using StudentsSocialMedia.Web.ViewModels.SearchGroups;

    public interface IGroupsService
    {
        Task Create(CreateGroupInputModel input, string path);

        T GetById<T>(string id);

        IEnumerable<T> GetAllById<T>(string id);

        IEnumerable<T> GetAllBySubjects<T>(IEnumerable<string> subjects, string id);

        IEnumerable<T> GetAllMembersById<T>(string id);

        IEnumerable<T> GetAllByNameAndSubjects<T>(SearchInputModel input);

        IEnumerable<SelectListItem> GetAllAsListItems(string id);

        Task Follow(string groupId, string userId);

        Task Unfollow(string groupId, string userId);

        Task Delete(string id);

        Task Update(string id, EditGroupInputModel input);
    }
}
