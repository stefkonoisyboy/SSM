namespace StudentsSocialMedia.Web.ViewModels.Images
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Web.ViewModels.Profiles;

    public class AllImagesByIdListViewModel
    {
        public UserViewModel UserInfo { get; set; }

        public IEnumerable<AllImagesByIdViewModel> Images { get; set; }
    }
}
