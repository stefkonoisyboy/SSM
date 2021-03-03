namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class Answer : BaseDeletableModel<string>
    {
        public Answer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }

        public string QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool IsCorrect { get; set; }
    }
}
