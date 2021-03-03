namespace StudentsSocialMedia.Web.ViewModels.Forum.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AllCategoriesViewModel : IMapFrom<ForumCategory>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string RemoteImageUrl { get; set; }

        public int PostsCount { get; set; }
    }
}
