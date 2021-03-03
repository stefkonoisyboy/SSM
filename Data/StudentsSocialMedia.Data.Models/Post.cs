namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class Post : BaseDeletableModel<string>
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
        }

        public string Content { get; set; }

        public string SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public string GroupId { get; set; }

        public virtual Group Group { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
    }
}
