namespace StudentsSocialMedia.Web.ViewModels.Hobbies
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllHobbiesByIdViewModel : IMapFrom<UserHobby>
    {
        public string HobbyName { get; set; }
    }
}
