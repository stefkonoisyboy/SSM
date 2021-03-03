namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class Choice : BaseDeletableModel<string>
    {
        public Choice()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }

        public string QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public bool IsCorrect { get; set; }
    }
}
