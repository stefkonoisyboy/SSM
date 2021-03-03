namespace StudentsSocialMedia.Web.ViewModels.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Data.Models.Enumerations;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Hobbies;
    using StudentsSocialMedia.Web.ViewModels.Subjects;

    public class AboutViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string TownName { get; set; }

        public string Gender { get; set; }

        public string ImageUrl { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<AllHobbiesByIdViewModel> Hobbies { get; set; }

        public IEnumerable<AllSubjectsByIdViewModel> Subjects { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, AboutViewModel>()
                .ForMember(about => about.ImageUrl, opt => opt.MapFrom(au => au.Images.FirstOrDefault().RemoteImageUrl != null
                    ? au.Images.FirstOrDefault().RemoteImageUrl : $"/images/users/{au.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Id}{au.Images.OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.Id.StartsWith("profile")).Extension}"));
        }
    }
}
