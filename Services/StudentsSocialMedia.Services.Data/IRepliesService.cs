namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Web.ViewModels.Home;
    using StudentsSocialMedia.Web.ViewModels.Replies;

    public interface IRepliesService
    {
        IEnumerable<T> GetAll<T>(string id);

        Task Create(CreateReplyInputModel input);
    }
}
