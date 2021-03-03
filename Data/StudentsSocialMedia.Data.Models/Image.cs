namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
