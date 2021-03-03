namespace StudentsSocialMedia.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AllViewModelList
    {
        public IEnumerable<AllViewModel> Comments { get; set; }

        public int CommentsCount => this.Comments.Count();
    }
}
