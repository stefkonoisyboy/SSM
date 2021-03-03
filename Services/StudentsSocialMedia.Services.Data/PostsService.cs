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
    using StudentsSocialMedia.Web.ViewModels.Comments;
    using StudentsSocialMedia.Web.ViewModels.Home;
    using StudentsSocialMedia.Web.ViewModels.Posts;
    using StudentsSocialMedia.Web.ViewModels.Replies;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task CreateAsync(CreatePostInputModel input)
        {
            Post post = new Post
            {
                Content = input.Content,
                SubjectId = input.SubjectId,
                CreatorId = input.CreatorId,
                GroupId = input.GroupId,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            IEnumerable<T> posts = this.postsRepository
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .To<T>()
                .ToList();

            return posts;
        }

        public IEnumerable<T> GetAllByGroupId<T>(string id)
        {
            IEnumerable<T> posts = this.postsRepository
                .All()
                .Where(p => p.GroupId == id)
                .OrderByDescending(p => p.CreatedOn)
                .To<T>()
                .ToList();

            return posts;
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> posts = this.postsRepository
                .All()
                .Where(p => p.CreatorId == id)
                .OrderByDescending(p => p.CreatedOn)
                .To<T>()
                .ToList();

            return posts;
        }

        public IEnumerable<T> GetAllWithGroup<T>()
        {
            IEnumerable<T> posts = this.postsRepository
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .Where(p => p.GroupId != null)
                .To<T>()
                .ToList();

            return posts;
        }
    }
}
