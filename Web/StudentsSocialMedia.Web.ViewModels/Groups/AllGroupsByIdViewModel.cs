namespace StudentsSocialMedia.Web.ViewModels.Groups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllGroupsByIdViewModel : IMapFrom<MemberGroup>, IHaveCustomMappings
    {
        public string GroupId { get; set; }

        public string GroupName { get; set; }

        public string GroupCreatorUserName { get; set; }

        public string ImageUrl { get; set; }

        public int GroupMembersCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MemberGroup, AllGroupsByIdViewModel>()
                .ForMember(all => all.ImageUrl, opt => opt.MapFrom(g => g.Group.Images.OrderByDescending(i => i.CreatedOn)
                .Where(i => i.Id.StartsWith("group")).FirstOrDefault().RemoteImageUrl != null ? g.Group.Images.OrderByDescending(i => i.CreatedOn)
                .Where(i => i.Id.StartsWith("group")).FirstOrDefault().RemoteImageUrl :
                $"/images/groups/{g.Group.Images.OrderByDescending(i => i.CreatedOn).Where(i => i.Id.StartsWith("group")).FirstOrDefault().Id}{g.Group.Images.OrderByDescending(i => i.CreatedOn).Where(i => i.Id.StartsWith("group")).FirstOrDefault().Extension}"));
        }
    }
}
