namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class Like : BaseDeletableModel<string>
    {
        public Like()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public string PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
