namespace StudentsSocialMedia.Web.ViewModels.Forum.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllCommentsByIdViewModel : IMapFrom<ForumComment>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string UserUserName { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ForumComment, AllCommentsByIdViewModel>()
                .ForMember(all => all.ImageUrl, opt => opt.MapFrom(p => p.User.Images.FirstOrDefault().RemoteImageUrl != null
                    ? p.User.Images.FirstOrDefault().RemoteImageUrl : $"/images/users/{p.User.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Id}{p.User.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Extension}"));
        }
    }
}
