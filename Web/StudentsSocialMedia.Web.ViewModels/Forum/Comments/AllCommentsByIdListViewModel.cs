namespace StudentsSocialMedia.Web.ViewModels.Forum.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllCommentsByIdListViewModel
    {
        public IEnumerable<AllCommentsByIdViewModel> Comments { get; set; }
    }
}
