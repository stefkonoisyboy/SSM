namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        Task Create(CreateMessageInputModel input);

        IEnumerable<T> GetAllById<T>(string id);
    }
}
