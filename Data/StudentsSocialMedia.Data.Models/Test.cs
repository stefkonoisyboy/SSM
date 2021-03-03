namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class Test : BaseDeletableModel<string>
    {
        public Test()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Participants = new HashSet<TestParticipant>();
            this.Questions = new HashSet<Question>();
        }

        public string Name { get; set; }

        public string SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<TestParticipant> Participants { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
