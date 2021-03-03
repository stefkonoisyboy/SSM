namespace StudentsSocialMedia.Web.ViewModels.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllMessagesByIdViewModel : IMapFrom<Message>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, AllMessagesByIdViewModel>()
                .ForMember(about => about.ImageUrl, opt => opt.MapFrom(au => au.User.Images.FirstOrDefault().RemoteImageUrl != null
                    ? au.User.Images.FirstOrDefault().RemoteImageUrl : $"/images/users/{au.User.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Id}{au.User.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Extension}"));
        }
    }
}
