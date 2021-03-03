namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class ForumCategory : BaseDeletableModel<string>
    {
        public ForumCategory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Posts = new HashSet<ForumPost>();
        }

        public string Title { get; set; }

        public string RemoteImageUrl { get; set; }

        public virtual ICollection<ForumPost> Posts { get; set; }
    }
}
