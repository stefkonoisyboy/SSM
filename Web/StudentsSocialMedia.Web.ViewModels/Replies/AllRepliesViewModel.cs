namespace StudentsSocialMedia.Web.ViewModels.Replies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllRepliesViewModel : IMapFrom<Reply>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUserName { get; set; }

        public string AuthorImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Reply, AllRepliesViewModel>()
                .ForMember(all => all.AuthorImageUrl, opt => opt.MapFrom(r => r.Author.Images.FirstOrDefault().RemoteImageUrl != null
                    ? r.Author.Images.FirstOrDefault().RemoteImageUrl : $"/images/users/{r.Author.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Id}{r.Author.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Extension}"));
        }
    }
}
