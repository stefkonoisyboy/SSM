namespace StudentsSocialMedia.Web.ViewModels.Groups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllMembersByIdViewModel : IMapFrom<MemberGroup>, IHaveCustomMappings
    {
        public string MemberId { get; set; }

        public string MemberTownName { get; set; }

        public string MemberUserName { get; set; }

        public string ImageUrl { get; set; }

        public int MemberFollowersCount { get; set; }

        public int MemberFollowingsCount { get; set; }

        public int MemberImagesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MemberGroup, AllMembersByIdViewModel>()
                .ForMember(u => u.ImageUrl, opt => opt.MapFrom(f => f.Member.Images.FirstOrDefault().RemoteImageUrl != null
                    ? f.Member.Images.FirstOrDefault().RemoteImageUrl : $"/images/users/{f.Member.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Id}{f.Member.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Extension}"));
        }
    }
}
