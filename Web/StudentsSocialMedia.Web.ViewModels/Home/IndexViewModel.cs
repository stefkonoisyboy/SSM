namespace StudentsSocialMedia.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Web.ViewModels.Posts;
    using StudentsSocialMedia.Web.ViewModels.Profiles;

    public class IndexViewModel
    {
        public IEnumerable<AllPostsViewModel> Posts { get; set; }

        public UserViewModel CurrentUserInfo { get; set; }
    }
}
