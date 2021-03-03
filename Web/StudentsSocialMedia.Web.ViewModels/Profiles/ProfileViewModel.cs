namespace StudentsSocialMedia.Web.ViewModels.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Web.ViewModels.Followers;
    using StudentsSocialMedia.Web.ViewModels.Images;
    using StudentsSocialMedia.Web.ViewModels.Posts;

    public class ProfileViewModel
    {
        public UserViewModel UserInfo { get; set; }

        public IEnumerable<AllPostsViewModel> Posts { get; set; }

        public IEnumerable<AllImagesViewModel> LastPhotos { get; set; }

        public IEnumerable<AllFollowersViewModel> Followers { get; set; }
    }
}
