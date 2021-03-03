namespace StudentsSocialMedia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Models;

    public class Question : BaseDeletableModel<string>
    {
        public Question()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Choices = new HashSet<Choice>();
            this.Answers = new HashSet<Answer>();
        }

        public string Text { get; set; }

        public string TestId { get; set; }

        public virtual Test Test { get; set; }

        public virtual ICollection<Choice> Choices { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
