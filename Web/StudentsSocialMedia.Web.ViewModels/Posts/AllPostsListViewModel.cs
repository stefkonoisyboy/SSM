namespace StudentsSocialMedia.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Services.Mapping;

    public class AllPostsListViewModel
    {
        public IEnumerable<AllPostsViewModel> Posts { get; set; }
    }
}
