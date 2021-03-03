namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentsSocialMedia.Web.ViewModels.Tests;

    public interface ITestsService
    {
        Task<string> Create(CreateTestInputModel input);

        Task Submit(string testId, string participantId);

        IEnumerable<SelectListItem> GetAllAsListItemsById(string id);

        IEnumerable<T> GetAllBySubjects<T>(IEnumerable<string> subjects, string id);

        IEnumerable<T> GetUsersInRanking<T>();

        Task<int> GetPoints(IFormCollection formCollection, string userId);

        Task IncreaseUserPoints(string userId, int points);
    }
}
