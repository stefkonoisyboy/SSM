namespace StudentsSocialMedia.Web.ViewModels.Forum.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllPostsByIdListViewModel
    {
        public IEnumerable<AllPostsByIdViewModel> Posts { get; set; }
    }
}
