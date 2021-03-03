namespace StudentsSocialMedia.Web.ViewModels.Likes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllLikesByIdViewModel : IMapFrom<Like>
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string PostId { get; set; }
    }
}
