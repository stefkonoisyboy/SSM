namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class AnswersService : IAnswersService
    {
        private readonly IDeletableEntityRepository<Answer> answersRepository;

        public AnswersService(IDeletableEntityRepository<Answer> answersRepository)
        {
            this.answersRepository = answersRepository;
        }

        public IEnumerable<T> GetAllById<T>(string questionId)
        {
            IEnumerable<T> answers = this.answersRepository
                .All()
                .Where(a => a.QuestionId == questionId)
                .To<T>()
                .ToList();

            return answers;
        }
    }
}
