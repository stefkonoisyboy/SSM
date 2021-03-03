namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class MemberGroup : BaseDeletableModel<string>
    {
        public MemberGroup()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string MemberId { get; set; }

        public virtual ApplicationUser Member { get; set; }

        public string GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
