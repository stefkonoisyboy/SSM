namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class Group : BaseDeletableModel<string>
    {
        public Group()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Members = new HashSet<MemberGroup>();
            this.Images = new HashSet<Image>();
            this.Posts = new HashSet<Post>();
            this.Messages = new HashSet<Message>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public string SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ICollection<MemberGroup> Members { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
