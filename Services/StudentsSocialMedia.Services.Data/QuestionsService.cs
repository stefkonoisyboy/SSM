namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Tests;

    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> questionsRepository;
        private readonly IDeletableEntityRepository<Choice> choicesRepository;

        public QuestionsService(IDeletableEntityRepository<Question> questionsRepository, IDeletableEntityRepository<Choice> choicesRepository)
        {
            this.questionsRepository = questionsRepository;
            this.choicesRepository = choicesRepository;
        }

        public async Task<string> Create(CreateQuestionInputModel input)
        {
            Question question = new Question
            {
                Text = input.Text,
                TestId = input.TestId,
            };

            foreach (var inputChoice in input.Choices)
            {
                Choice choice = new Choice
                {
                    Text = inputChoice.Text,
                    IsCorrect = inputChoice.IsCorrect.ToLower() == "yes",
                    QuestionId = question.Id,
                };

                await this.choicesRepository.AddAsync(choice);
            }

            await this.questionsRepository.AddAsync(question);
            await this.questionsRepository.SaveChangesAsync();
            await this.choicesRepository.SaveChangesAsync();

            return question.Id;
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> questions = this.questionsRepository
                .All()
                .OrderBy(q => q.CreatedOn)
                .Where(q => q.TestId == id)
                .To<T>()
                .ToList();

            return questions;
        }
    }
}
