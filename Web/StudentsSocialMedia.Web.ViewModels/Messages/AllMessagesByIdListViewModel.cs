namespace StudentsSocialMedia.Web.ViewModels.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllMessagesByIdListViewModel
    {
        public IEnumerable<AllMessagesByIdViewModel> Messages { get; set; }
    }
}
