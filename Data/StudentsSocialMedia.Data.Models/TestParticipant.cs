namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class TestParticipant : BaseDeletableModel<string>
    {
        public TestParticipant()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ParticipantId { get; set; }

        public virtual ApplicationUser Participant { get; set; }

        public string TestId { get; set; }

        public virtual Test Test { get; set; }
    }
}
