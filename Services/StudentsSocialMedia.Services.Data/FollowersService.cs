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
    using StudentsSocialMedia.Web.ViewModels.Followers;
    using StudentsSocialMedia.Web.ViewModels.SearchFollowers;

    public class FollowersService : IFollowersService
    {
        private readonly IDeletableEntityRepository<Follower> followersRepository;

        public FollowersService(IDeletableEntityRepository<Follower> followersRepository)
        {
            this.followersRepository = followersRepository;
        }

        public async Task Follow(string id, string userToBeFollowedId)
        {
            Follower follower = new Follower
            {
                FollowingId = userToBeFollowedId,
                FollowersId = id,
            };

            await this.followersRepository.AddAsync(follower);
            await this.followersRepository.SaveChangesAsync();
        }

        public IEnumerable<T> Get12ById<T>(string id)
        {
            IEnumerable<T> followers = this.followersRepository
               .All()
               .Take(12)
               .Where(f => f.FollowingId == id)
               .To<T>()
               .ToList();

            return followers;
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> followers = this.followersRepository
                .All()
                .Where(f => f.FollowingId == id)
                .To<T>()
                .ToList();

            return followers;
        }

        public IEnumerable<T> SearchAllByUsername<T>(SearchInputModel input)
        {
            IEnumerable<T> followers = this.followersRepository
                .All()
                .Where(f => f.Followers.UserName.Contains(input.UserName) && f.FollowingId == input.UserId)
                .To<T>()
                .ToList();

            return followers;
        }

        public async Task Unfollow(string id, string userToBeUnfollowedId)
        {
            Follower follower = this.followersRepository.All().FirstOrDefault(f => f.FollowingId == userToBeUnfollowedId && f.FollowersId == id);
            this.followersRepository.Delete(follower);

            await this.followersRepository.SaveChangesAsync();
        }
    }
}
