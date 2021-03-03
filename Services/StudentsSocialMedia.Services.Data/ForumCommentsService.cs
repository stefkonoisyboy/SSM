namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Ganss.XSS;
    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Forum.Comments;

    public class ForumCommentsService : IForumCommentsService
    {
        private readonly IDeletableEntityRepository<ForumComment> forumCommentsRepository;

        public ForumCommentsService(IDeletableEntityRepository<ForumComment> forumCommentsRepository)
        {
            this.forumCommentsRepository = forumCommentsRepository;
        }

        public async Task Create(CreateCommentInputModel input)
        {
            ForumComment forumComment = new ForumComment
            {
                Content = new HtmlSanitizer().Sanitize(input.Content),
                UserId = input.UserId,
                PostId = input.PostId,
            };

            await this.forumCommentsRepository.AddAsync(forumComment);
            await this.forumCommentsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> forumComments = this.forumCommentsRepository
                .All()
                .Where(c => c.PostId == id)
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToList();

            return forumComments;
        }
    }
}
