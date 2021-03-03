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
    using StudentsSocialMedia.Web.ViewModels.Likes;

    public class LikesService : ILikesService
    {
        private readonly IDeletableEntityRepository<Like> likesRepository;

        public LikesService(IDeletableEntityRepository<Like> likesRepository)
        {
            this.likesRepository = likesRepository;
        }

        public async Task Create(CreateLikeInputModel input)
        {
            Like like = new Like
            {
                CreatorId = input.CreatorId,
                PostId = input.PostId,
            };

            await this.likesRepository.AddAsync(like);
            await this.likesRepository.SaveChangesAsync();
        }

        public async Task Delete(string postId, string userId)
        {
            Like like = this.likesRepository.All().FirstOrDefault(l => l.PostId == postId && l.CreatorId == userId);
            this.likesRepository.Delete(like);
            await this.likesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> likes = this.likesRepository
                .All()
                .Where(l => l.PostId == id)
                .To<T>()
                .ToList();

            return likes;
        }

        public int GetCount(string id)
        {
            return this.likesRepository
                .All()
                .Where(l => l.PostId == id)
                .ToList()
                .Count();
        }
    }
}
