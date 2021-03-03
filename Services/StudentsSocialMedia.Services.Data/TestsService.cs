namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Tests;

    public class TestsService : ITestsService
    {
        private readonly IDeletableEntityRepository<Test> testsRepository;
        private readonly IDeletableEntityRepository<TestParticipant> testParticipantsRepository;
        private readonly IDeletableEntityRepository<Question> questionsRepository;
        private readonly IDeletableEntityRepository<Choice> choicesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Answer> answersRepository;

        public TestsService(
            IDeletableEntityRepository<Test> testsRepository,
            IDeletableEntityRepository<TestParticipant> testParticipantsRepository,
            IDeletableEntityRepository<Question> questionsRepository,
            IDeletableEntityRepository<Choice> choicesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Answer> answersRepository)
        {
            this.testsRepository = testsRepository;
            this.testParticipantsRepository = testParticipantsRepository;
            this.questionsRepository = questionsRepository;
            this.choicesRepository = choicesRepository;
            this.usersRepository = usersRepository;
            this.answersRepository = answersRepository;
        }

        public async Task<string> Create(CreateTestInputModel input)
        {
            Test test = new Test
            {
                Name = input.Name,
                SubjectId = input.SubjectId,
                CreatorId = input.CreatorId,
            };

            await this.testsRepository.AddAsync(test);
            await this.testsRepository.SaveChangesAsync();

            return test.Id;
        }

        public IEnumerable<SelectListItem> GetAllAsListItemsById(string id)
        {
            IEnumerable<SelectListItem> tests = this.testsRepository
                .All()
                .OrderBy(t => t.Name)
                .Where(t => t.CreatorId == id)
                .Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id,
                })
                .ToList();

            return tests;
        }

        public IEnumerable<T> GetAllBySubjects<T>(IEnumerable<string> subjects, string id)
        {
            IEnumerable<T> tests = this.testsRepository
                .All()
                .OrderByDescending(t => t.CreatedOn)
                .Where(t => subjects.Contains(t.Subject.Name) && !t.Participants.Any(p => p.ParticipantId == id))
                .To<T>()
                .ToList();

            return tests;
        }

        public async Task<int> GetPoints(IFormCollection formCollection, string userId)
        {
            int score = 0;

            string[] questionIds = formCollection["questionId"];
            foreach (var questionId in questionIds)
            {
                string choiceIdCorrect = this.choicesRepository
                    .All()
                    .FirstOrDefault(c => c.QuestionId == questionId && c.IsCorrect)
                    .Id;

                if (choiceIdCorrect == formCollection["question_" + questionId])
                {
                    score++;
                }

                string choiceMadeId = formCollection["question_" + questionId];

                Answer answer = new Answer
                {
                    QuestionId = questionId,
                    UserId = userId,
                    Text = this.choicesRepository
                    .All()
                    .FirstOrDefault(c => c.Id == choiceMadeId).Text,
                    IsCorrect = choiceIdCorrect == choiceMadeId,
                };

                await this.answersRepository.AddAsync(answer);
            }

            await this.answersRepository.SaveChangesAsync();
            return score;
        }

        public IEnumerable<T> GetUsersInRanking<T>()
        {
            IEnumerable<T> users = this.usersRepository
                .All()
                .Where(u => u.Roles.FirstOrDefault().RoleId == "25d153f8-a7bd-464b-9a81-370e37c8ebed")
                .OrderByDescending(u => u.TestPoints)
                .To<T>()
                .ToList();

            return users;
        }

        public async Task IncreaseUserPoints(string userId, int points)
        {
            ApplicationUser user = this.usersRepository.All().FirstOrDefault(u => u.Id == userId);
            user.TestPoints += points;
            await this.usersRepository.SaveChangesAsync();
        }

        public async Task Submit(string testId, string participantId)
        {
            TestParticipant testParticipant = new TestParticipant
            {
                TestId = testId,
                ParticipantId = participantId,
            };

            await this.testParticipantsRepository.AddAsync(testParticipant);
            await this.testParticipantsRepository.SaveChangesAsync();
        }
    }
}
