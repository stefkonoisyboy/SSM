namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class ForumPost : BaseDeletableModel<string>
    {
        public ForumPost()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<ForumComment>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string CategoryId { get; set; }

        public virtual ForumCategory Category { get; set; }

        public virtual ICollection<ForumComment> Comments { get; set; }
    }
}
