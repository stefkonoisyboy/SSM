namespace StudentsSocialMedia.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Comments;
    using StudentsSocialMedia.Web.ViewModels.Likes;

    public class AllPostsViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string SubjectName { get; set; }

        public string CreatorUserName { get; set; }

        public string CreatorImageUrl { get; set; }

        public string CreatorId { get; set; }

        public int LikesCount { get; set; }

        public IEnumerable<AllLikesByIdViewModel> Likes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, AllPostsViewModel>()
                .ForMember(all => all.CreatorImageUrl, opt => opt.MapFrom(p => p.Creator.Images.FirstOrDefault().RemoteImageUrl != null
                    ? p.Creator.Images.FirstOrDefault().RemoteImageUrl : $"/images/users/{p.Creator.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Id}{p.Creator.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Extension}"));
        }
    }
}
