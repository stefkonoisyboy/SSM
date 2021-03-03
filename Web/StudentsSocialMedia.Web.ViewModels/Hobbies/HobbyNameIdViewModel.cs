namespace StudentsSocialMedia.Web.ViewModels.Hobbies
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class HobbyNameIdViewModel : IMapFrom<Hobby>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
