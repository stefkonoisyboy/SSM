namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class Follower : BaseDeletableModel<string>
    {
        public Follower()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string FollowersId { get; set; }

        public virtual ApplicationUser Followers { get; set; }

        public string FollowingId { get; set; }

        public virtual ApplicationUser Following { get; set; }
    }
}
