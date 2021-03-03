namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IAnswersService
    {
        IEnumerable<T> GetAllById<T>(string questionId);
    }
}
