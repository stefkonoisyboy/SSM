namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Messages;

    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;

        public MessagesService(IDeletableEntityRepository<Message> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task Create(CreateMessageInputModel input)
        {
            Message message = new Message
            {
                Content = input.Content,
                UserId = input.UserId,
                GroupId = input.GroupId,
            };

            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> messages = this.messagesRepository
                .All()
                .OrderBy(m => m.CreatedOn)
                .Where(m => m.GroupId == id)
                .To<T>()
                .ToList();

            return messages;
        }
    }
}
