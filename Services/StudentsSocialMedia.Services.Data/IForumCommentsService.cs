namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Web.ViewModels.Forum.Comments;

    public interface IForumCommentsService
    {
        Task Create(CreateCommentInputModel input);

        IEnumerable<T> GetAllById<T>(string id);
    }
}
