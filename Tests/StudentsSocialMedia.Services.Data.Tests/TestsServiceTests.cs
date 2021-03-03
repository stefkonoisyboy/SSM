namespace StudentsSocialMedia.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Moq;
    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Web.ViewModels.Tests;
    using Xunit;

    public class TestsServiceTests
    {
        [Fact]
        public async Task Create_Should_Work_Correctly()
        {
            // Arrange
            List<Test> tests = new List<Test>();
            var mockedRepository = new Mock<IDeletableEntityRepository<Test>>();
            mockedRepository.Setup(x => x.AddAsync(It.IsAny<Test>())).Callback(
                (Test test) => tests.Add(test));
            ITestsService testsService = new TestsService(mockedRepository.Object, null, null, null, null, null);
            CreateTestInputModel input = new CreateTestInputModel
            {
                Name = "Some name",
                CreatorId = "stefko",
                SubjectId = "maths",
            };

            // Act
            await testsService.Create(input);

            // Assert
            Assert.Equal(1, tests.Count());
        }

        [Theory]
        [InlineData("1", "stefko")]
        public async Task Submit_Should_Work_Correctly(string testId, string participantId)
        {
            // Arrange
            List<TestParticipant> testParticipants = new List<TestParticipant>();
            var mockedRepository = new Mock<IDeletableEntityRepository<TestParticipant>>();
            mockedRepository.Setup(x => x.AddAsync(It.IsAny<TestParticipant>())).Callback(
                (TestParticipant testParticipant) => testParticipants.Add(testParticipant));
            ITestsService testsService = new TestsService(null, mockedRepository.Object, null, null, null, null);

            // Act
            await testsService.Submit(testId, participantId);

            // Assert
            Assert.Equal(1, testParticipants.Count());
        }

        [Theory]
        [InlineData("stefko", 3)]
        public async Task IncreasePoints_Should_Work_Correctly(string userId, int points)
        {
            List<ApplicationUser> testData = this.GetUserTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ITestsService testsService = new TestsService(null, null, null, null, mockedRepository.Object, null);

            await testsService.IncreaseUserPoints(userId, points);
            ApplicationUser user = testData.FirstOrDefault(x => x.Id == userId);

            Assert.Equal(8, user.TestPoints);
        }

        [Theory]
        [InlineData("stefko")]
        public void GetAllAsItems_Should_Work_Correctly(string id)
        {
            // Arrange
            List<Test> testData = new List<Test>();
            var mockedRepository = new Mock<IDeletableEntityRepository<Test>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ITestsService testsService = new TestsService(mockedRepository.Object, null, null, null, null, null);
            IEnumerable<string> expectedIds = testData.OrderBy(x => x.Name).Where(x => x.CreatorId == id).Select(x => x.Id);
            IEnumerable<string> expectedNames = testData.OrderBy(x => x.Name).Where(x => x.CreatorId == id).Select(x => x.Name);

            // Act
            IEnumerable<SelectListItem> actual = testsService.GetAllAsListItemsById(id);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Value));
            Assert.Equal(expectedNames, actual.Select(x => x.Text));
        }

        [Theory]
        [InlineData("miro")]
        public void GetAllBySubjects_Should_Work_Correctly(string id)
        {
            // Arrange
            List<Test> testData = this.GetTestsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Test>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ITestsService testsService = new TestsService(mockedRepository.Object, null, null, null, null, null);
            IEnumerable<string> subjects = new List<string>() { "Mathematics", "P.E." };
            IEnumerable<string> expectedIds = testData.OrderByDescending(x => x.CreatedOn).Where(x => subjects.Contains(x.Subject.Name) && !x.Participants.Any(p => p.ParticipantId == id)).Select(x => x.Id);
            IEnumerable<string> expectedNames = testData.OrderByDescending(x => x.CreatedOn).Where(x => subjects.Contains(x.Subject.Name) && !x.Participants.Any(p => p.ParticipantId == id)).Select(x => x.Name);
            IEnumerable<string> expectedSubjectsNames = testData.OrderByDescending(x => x.CreatedOn).Where(x => subjects.Contains(x.Subject.Name) && !x.Participants.Any(p => p.ParticipantId == id)).Select(x => x.Subject.Name);

            // Act
            IEnumerable<AllTestsBySubjectsViewModel> actual = testsService.GetAllBySubjects<AllTestsBySubjectsViewModel>(subjects, id);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedSubjectsNames, actual.Select(x => x.SubjectName));
        }

        [Fact]
        public void GetUsersInRanking_Should_Work_Correctly()
        {
            List<ApplicationUser> testData = this.GetUserTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ITestsService testsService = new TestsService(null, null, null, null, mockedRepository.Object, null);
            IEnumerable<string> expectedIds = testData.OrderByDescending(x => x.TestPoints).Where(x => x.Roles.FirstOrDefault().RoleId == "25d153f8-a7bd-464b-9a81-370e37c8ebed").Select(x => x.Id);
            IEnumerable<string> expectedUsernames = testData.OrderByDescending(x => x.TestPoints).Where(x => x.Roles.FirstOrDefault().RoleId == "25d153f8-a7bd-464b-9a81-370e37c8ebed").Select(x => x.UserName);
            IEnumerable<string> expectedFirstNames = testData.OrderByDescending(x => x.TestPoints).Where(x => x.Roles.FirstOrDefault().RoleId == "25d153f8-a7bd-464b-9a81-370e37c8ebed").Select(x => x.FirstName);
            IEnumerable<string> expectedLastNames = testData.OrderByDescending(x => x.TestPoints).Where(x => x.Roles.FirstOrDefault().RoleId == "25d153f8-a7bd-464b-9a81-370e37c8ebed").Select(x => x.LastName);
            IEnumerable<int> expectedPoints = testData.OrderByDescending(x => x.TestPoints).Where(x => x.Roles.FirstOrDefault().RoleId == "25d153f8-a7bd-464b-9a81-370e37c8ebed").Select(x => x.TestPoints);

            IEnumerable<RankingViewModel> actual = testsService.GetUsersInRanking<RankingViewModel>();

            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedFirstNames, actual.Select(x => x.FirstName));
            Assert.Equal(expectedLastNames, actual.Select(x => x.LastName));
            Assert.Equal(expectedUsernames, actual.Select(x => x.UserName));
            Assert.Equal(expectedPoints, actual.Select(x => x.TestPoints));
        }

        private List<ApplicationUser> GetUserTestData()
        {
            return new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    Id = "stefko",
                    UserName = "stefkonoisyboy",
                    FirstName = "Stefko",
                    LastName = "Tsonyovski",
                    TestPoints = 5,
                },
            };
        }

        private List<Test> GetTestsTestData()
        {
            return new List<Test>()
            {
                new Test
                {
                    Id = "1",
                    Name = "Some name",
                    CreatedOn = DateTime.UtcNow,
                    Creator = new ApplicationUser
                    {
                        Id = "stefko",
                        UserName = "stefkonoisyboy",
                    },
                    Subject = new Subject
                    {
                        Id = "maths",
                        Name = "Mathematics",
                    },
                    CreatorId = "stefko",
                    SubjectId = "maths",
                    Participants = new HashSet<TestParticipant>(),
                },
            };
        }
    }
}
