namespace StudentsSocialMedia.Web.ViewModels.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserProfileImageUrl { get; set; }

        public string UserName { get; set; }

        public string TownName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(u => u.UserProfileImageUrl, opt => opt.MapFrom(au => au.Images.FirstOrDefault().RemoteImageUrl != null
                    ? au.Images.FirstOrDefault().RemoteImageUrl : $"/images/users/{au.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Id}{au.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Extension}"));
        }
    }
}
