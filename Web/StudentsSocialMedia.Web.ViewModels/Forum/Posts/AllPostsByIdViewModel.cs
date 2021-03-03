namespace StudentsSocialMedia.Web.ViewModels.Forum.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using Ganss.XSS;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllPostsByIdViewModel : IMapFrom<ForumPost>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserUserName { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryTitle { get; set; }

        public int CommentsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ForumPost, AllPostsByIdViewModel>()
                .ForMember(all => all.ImageUrl, opt => opt.MapFrom(p => p.User.Images.FirstOrDefault().RemoteImageUrl != null
                    ? p.User.Images.FirstOrDefault().RemoteImageUrl : $"/images/users/{p.User.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Id}{p.User.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Extension}"));
        }
    }
}
