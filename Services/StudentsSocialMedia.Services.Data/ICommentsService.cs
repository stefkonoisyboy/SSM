namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Web.ViewModels.Home;

    public interface ICommentsService
    {
        IEnumerable<T> GetAll<T>(string postId);

        Task CreateAsync(CreateCommentInputModel input);
    }
}
