namespace StudentsSocialMedia.Web.ViewModels.Groups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Posts;

    public class ActivityByIdViewModel : IMapFrom<Group>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CreatorUserName { get; set; }

        public string ImageUrl { get; set; }

        public int MembersCount { get; set; }

        public IEnumerable<AllPostsViewModel> Posts { get; set; }

        public IEnumerable<AllMembersByIdViewModel> Members { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Group, ActivityByIdViewModel>()
                .ForMember(all => all.ImageUrl, opt => opt.MapFrom(g => g.Images.OrderByDescending(i => i.CreatedOn)
                .Where(i => i.Id.StartsWith("group")).FirstOrDefault().RemoteImageUrl != null ? g.Images.OrderByDescending(i => i.CreatedOn)
                .Where(i => i.Id.StartsWith("group")).FirstOrDefault().RemoteImageUrl :
                $"/images/groups/{g.Images.OrderByDescending(i => i.CreatedOn).Where(i => i.Id.StartsWith("group")).FirstOrDefault().Id}{g.Images.OrderByDescending(i => i.CreatedOn).Where(i => i.Id.StartsWith("group")).FirstOrDefault().Extension}"));
        }
    }
}
