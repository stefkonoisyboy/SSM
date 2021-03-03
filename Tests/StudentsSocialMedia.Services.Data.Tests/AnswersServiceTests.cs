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
    using StudentsSocialMedia.Web.ViewModels.Answers;
    using Xunit;

    public class AnswersServiceTests
    {
        [Theory]
        [InlineData("2")]
        public void GetAllById_Should_Return_Correct_Results(string id)
        {
            // Arrange
            List<Answer> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Answer>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IAnswersService answersService = new AnswersService(mockedRepository.Object);
            IEnumerable<string> expectedTexts = testData.Where(x => x.QuestionId == id).Select(x => x.Text);
            IEnumerable<string> expectedQuestionIds = testData.Where(x => x.QuestionId == id).Select(x => x.QuestionId);
            IEnumerable<string> expectedUserIds = testData.Where(x => x.QuestionId == id).Select(x => x.UserId);
            IEnumerable<bool> expectedIsCorrectResults = testData.Where(x => x.QuestionId == id).Select(x => x.IsCorrect);

            // Act
            IEnumerable<AllAnswersByIdViewModel> actual = answersService.GetAllById<AllAnswersByIdViewModel>(id);

            // Assert
            Assert.Equal(expectedTexts, actual.Select(x => x.Text));
            Assert.Equal(expectedQuestionIds, actual.Select(x => x.QuestionId));
            Assert.Equal(expectedUserIds, actual.Select(x => x.UserId));
            Assert.Equal(expectedIsCorrectResults, actual.Select(x => x.IsCorrect));
        }

        private List<Answer> GetTestData()
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer
                {
                    Text = "Some text",
                    QuestionId = "1",
                    UserId = "stefko",
                    IsCorrect = true,
                },
                new Answer
                {
                    Text = "Some text",
                    QuestionId = "2",
                    UserId = "miro",
                    IsCorrect = false,
                },
                new Answer
                {
                    Text = "Some text",
                    QuestionId = "2",
                    UserId = "gecata",
                    IsCorrect = false,
                },
            };

            return answers;
        }
    }
}
