namespace StudentsSocialMedia.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;
    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Web.ViewModels.Questions;
    using StudentsSocialMedia.Web.ViewModels.Tests;
    using Xunit;

    public class QuestionsServiceTests
    {
        [Fact]
        public async Task Create_Should_Work_Correctly()
        {
            // Arrange
            List<Choice> choices = new List<Choice>();
            List<Question> questions = new List<Question>();
            var mockedChoicesRepository = new Mock<IDeletableEntityRepository<Choice>>();
            var mockedQuestionsRepository = new Mock<IDeletableEntityRepository<Question>>();
            mockedChoicesRepository.Setup(x => x.AddAsync(It.IsAny<Choice>())).Callback(
                (Choice choice) => choices.Add(choice));
            mockedQuestionsRepository.Setup(x => x.AddAsync(It.IsAny<Question>())).Callback(
                (Question question) => questions.Add(question));
            IQuestionsService questionsService = new QuestionsService(mockedQuestionsRepository.Object, mockedChoicesRepository.Object);
            CreateQuestionInputModel inputFirst = new CreateQuestionInputModel
            {
                Text = "Some text",
                TestId = "1",
                Choices = new List<CreateChoiceInputModel>()
                {
                    new CreateChoiceInputModel
                    {
                        IsCorrect = "True",
                        Text = "Some choice text",
                    },
                    new CreateChoiceInputModel
                    {
                        IsCorrect = "False",
                        Text = "Some choice text",
                    },
                },
            };
            CreateQuestionInputModel inputSecond = new CreateQuestionInputModel
            {
                Text = "Some text",
                TestId = "2",
                Choices = new List<CreateChoiceInputModel>()
                {
                    new CreateChoiceInputModel
                    {
                        IsCorrect = "True",
                        Text = "Some choice text",
                    },
                    new CreateChoiceInputModel
                    {
                        IsCorrect = "False",
                        Text = "Some choice text",
                    },
                    new CreateChoiceInputModel
                    {
                        IsCorrect = "False",
                        Text = "Some choice text",
                    },
                },
            };

            // Act
            await questionsService.Create(inputFirst);
            await questionsService.Create(inputSecond);

            // Assert
            Assert.Equal(2, questions.Count());
            Assert.Equal(5, choices.Count());
        }

        [Theory]
        [InlineData("Maths")]
        public void GetAllById_Should_Return_Correct_Results(string id)
        {
            // Arrange
            List<Question> testData = new List<Question>();
            var mockedRepository = new Mock<IDeletableEntityRepository<Question>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IQuestionsService questionsService = new QuestionsService(mockedRepository.Object, null);
            IEnumerable<string> expectedIds = testData.Where(x => x.TestId == id).OrderBy(x => x.CreatedOn).Select(x => x.Id);
            IEnumerable<string> expectedTexts = testData.Where(x => x.TestId == id).OrderBy(x => x.CreatedOn).Select(x => x.Text);

            // Act
            IEnumerable<AllQuestionsByIdViewModel> actual = questionsService.GetAllById<AllQuestionsByIdViewModel>(id);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedTexts, actual.Select(x => x.Text));
        }

        private List<Question> GetTestData()
        {
            List<Question> questions = new List<Question>()
            {
                new Question
                {
                    Id = "1",
                    Text = "Some text",
                    TestId = "Maths",
                    CreatedOn = DateTime.UtcNow,
                },
                new Question
                {
                    Id = "2",
                    Text = "Some text",
                    TestId = "Maths",
                    CreatedOn = DateTime.UtcNow,
                },
                new Question
                {
                    Id = "3",
                    Text = "Some text",
                    TestId = "Informatics",
                    CreatedOn = DateTime.UtcNow,
                },
                new Question
                {
                    Id = "4",
                    Text = "Some text",
                    TestId = "BG",
                    CreatedOn = DateTime.UtcNow,
                },
                new Question
                {
                    Id = "5",
                    Text = "Some text",
                    TestId = "Maths",
                    CreatedOn = DateTime.UtcNow,
                },
            };

            return questions;
        }
    }
}
