namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class ForumComment : BaseDeletableModel<string>
    {
        public ForumComment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Content { get; set; }

        public string PostId { get; set; }

        public virtual ForumPost Post { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
