namespace StudentsSocialMedia.Web.ViewModels.Followers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Web.ViewModels.Profiles;
    using StudentsSocialMedia.Web.ViewModels.SearchFollowers;

    public class AllFollowersListViewModel
    {
        public UserViewModel UserInfo { get; set; }

        public SearchInputModel Input { get; set; }

        public IEnumerable<AllFollowersViewModel> Followers { get; set; }
    }
}
