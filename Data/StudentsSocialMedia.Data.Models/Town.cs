namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class Town: BaseDeletableModel<string>
    {
        public Town()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Users = new HashSet<ApplicationUser>();
        }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
