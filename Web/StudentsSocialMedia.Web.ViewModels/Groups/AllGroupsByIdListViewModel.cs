namespace StudentsSocialMedia.Web.ViewModels.Groups
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Web.ViewModels.Profiles;

    public class AllGroupsByIdListViewModel
    {
        public UserViewModel UserInfo { get; set; }

        public IEnumerable<AllGroupsByIdViewModel> Groups { get; set; }
    }
}
