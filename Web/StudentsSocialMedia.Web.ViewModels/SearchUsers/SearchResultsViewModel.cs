namespace StudentsSocialMedia.Web.ViewModels.SearchUsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Followers;

    public class SearchResultsViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string TownName { get; set; }

        public string ImageUrl { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingsCount { get; set; }

        public int ImagesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, SearchResultsViewModel>()
                .ForMember(u => u.ImageUrl, opt => opt.MapFrom(f => f.Images.FirstOrDefault().RemoteImageUrl != null
                    ? f.Images.FirstOrDefault().RemoteImageUrl : $"/images/users/{f.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Id}{f.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Extension}"));
        }
    }
}
