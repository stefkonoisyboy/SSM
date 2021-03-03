namespace StudentsSocialMedia.Web.ViewModels.Followers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllFollowersViewModel : IMapFrom<Follower>, IHaveCustomMappings
    {
        public string FollowersId { get; set; }

        public string FollowersUserName { get; set; }

        public string FollowersTownName { get; set; }

        public string ImageUrl { get; set; }

        public int FollowersFollowersCount { get; set; }

        public int FollowersFollowingsCount { get; set; }

        public int FollowersImagesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Follower, AllFollowersViewModel>()
                .ForMember(u => u.ImageUrl, opt => opt.MapFrom(f => f.Followers.Images.FirstOrDefault().RemoteImageUrl != null
                    ? f.Followers.Images.FirstOrDefault().RemoteImageUrl : $"/images/users/{f.Followers.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Id}{f.Followers.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Extension}"));
        }
    }
}
