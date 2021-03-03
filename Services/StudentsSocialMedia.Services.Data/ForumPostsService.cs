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
    using StudentsSocialMedia.Web.ViewModels.Forum.Posts;

    public class ForumPostsService : IForumPostsService
    {
        private readonly IDeletableEntityRepository<ForumPost> forumPostsRepository;

        public ForumPostsService(IDeletableEntityRepository<ForumPost> forumPostsRepository)
        {
            this.forumPostsRepository = forumPostsRepository;
        }

        public async Task Create(CreatePostInputModel input)
        {
            ForumPost forumPost = new ForumPost
            {
                Title = input.Title,
                Content = new HtmlSanitizer().Sanitize(input.Content),
                UserId = input.UserId,
                CategoryId = input.CategoryId,
            };

            await this.forumPostsRepository.AddAsync(forumPost);
            await this.forumPostsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> posts = this.forumPostsRepository
                .All()
                .Where(p => p.CategoryId == id)
                .OrderByDescending(p => p.CreatedOn)
                .To<T>()
                .ToList();

            return posts;
        }
    }
}
