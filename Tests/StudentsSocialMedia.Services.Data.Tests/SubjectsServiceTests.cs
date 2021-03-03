namespace StudentsSocialMedia.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Moq;
    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Web.ViewModels.Subjects;
    using Xunit;

    public class SubjectsServiceTests
    {
        [Fact]
        public async Task Create_Should_Work_Correctly()
        {
            // Arrange
            List<UserSubject> list = new List<UserSubject>();
            var mockedRepository = new Mock<IDeletableEntityRepository<UserSubject>>();
            mockedRepository.Setup(x => x.AddAsync(It.IsAny<UserSubject>())).Callback(
                (UserSubject userSubject) => list.Add(userSubject));
            ISubjectsService subjectsService = new SubjectsService(null, mockedRepository.Object);
            CreateSubjectInputModel inputFirst = new CreateSubjectInputModel
            {
                UserId = "stefko",
                Subjects = new List<string>()
                {
                    "1", "2", "3",
                },
            };
            CreateSubjectInputModel inputSecond = new CreateSubjectInputModel
            {
                UserId = "miro",
                Subjects = new List<string>()
                {
                    "4", "2",
                },
            };

            // Act
            await subjectsService.Create(inputFirst);
            await subjectsService.Create(inputSecond);

            // Assert
            Assert.Equal(5, list.Count());
        }

        [Fact]
        public void GetAll_Should_Return_Correct_Results()
        {
            // Arrange
            List<Subject> testData = this.GetSubjectsTestData();
            var mockedSubjectsRepository = new Mock<IDeletableEntityRepository<Subject>>();
            mockedSubjectsRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ISubjectsService subjectsService = new SubjectsService(mockedSubjectsRepository.Object, null);
            IEnumerable<string> expectedIds = testData.OrderBy(x => x.Name).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.OrderBy(x => x.Name).Select(x => x.Name).ToList();

            // Act
            IEnumerable<SelectListItem> actualItems = subjectsService.GetAll();

            // Assert
            Assert.Equal(expectedIds, actualItems.Select(x => x.Value));
            Assert.Equal(expectedNames, actualItems.Select(x => x.Text));
        }

        [Theory]
        [InlineData("stefko")]
        public void GetAllById_Should_Return_Correct_Results(string id)
        {
            // Arrange
            List<UserSubject> testData = this.GetUserSubjectsTestData();
            var mockedUserSubjectsRepository = new Mock<IDeletableEntityRepository<UserSubject>>();
            mockedUserSubjectsRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ISubjectsService subjectsService = new SubjectsService(null, mockedUserSubjectsRepository.Object);
            IEnumerable<string> expectedNames = testData.Where(x => x.UserId == id).Select(x => x.Subject.Name).ToList();

            // Act
            IEnumerable<string> result = subjectsService.GetAllById<AllSubjectsByIdViewModel>(id).Select(x => x.SubjectName).ToList();

            // Assert
            Assert.Equal(expectedNames, result);
        }

        [Theory]
        [InlineData("stefko")]
        public void GetAllNamesById_Should_Return_Correct_Results(string id)
        {
            // Arrange
            List<UserSubject> testData = this.GetUserSubjectsTestData();
            var mockedUserSubjectsRepository = new Mock<IDeletableEntityRepository<UserSubject>>();
            mockedUserSubjectsRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ISubjectsService subjectsService = new SubjectsService(null, mockedUserSubjectsRepository.Object);
            IEnumerable<string> expected = testData.Where(x => x.UserId == id).Select(x => x.Subject.Name).ToList();

            // Act
            IEnumerable<string> result = subjectsService.GetAllNamesById(id);

            // Assert
            Assert.Equal(expected, result);
        }

        private List<UserSubject> GetUserSubjectsTestData()
        {
            List<UserSubject> subjects = new List<UserSubject>()
            {
                new UserSubject
                {
                    Id = "dsgdfg",
                    Subject = new Subject
                    {
                        Id = "1",
                        Name = "Maths",
                    },
                    User = new ApplicationUser
                    {
                        Id = "stefko",
                        UserName = "stefkonoisyboy",
                    },
                    UserId = "stefko",
                    SubjectId = "1",
                },
                new UserSubject
                {
                    Id = "dsgdfgsdf",
                    Subject = new Subject
                    {
                        Id = "3",
                        Name = "Deutsch",
                    },
                    User = new ApplicationUser
                    {
                        Id = "miro",
                    },
                },
                new UserSubject
                {
                    Id = "dsgdfgdthghjklhuihguyftdrss",
                    Subject = new Subject
                    {
                        Id = "5",
                        Name = "I.T.",
                    },
                    UserId = "stefko",
                },
            };

            return subjects;
        }

        private List<Subject> GetSubjectsTestData()
        {
            List<Subject> subjects = new List<Subject>()
            {
                new Subject
                {
                    Id = "1",
                    Name = "Maths",
                },
                new Subject
                {
                    Id = "2",
                    Name = "Bulgarian",
                },
                new Subject
                {
                    Id = "3",
                    Name = "I.T.",
                },
            };

            return subjects;
        }
    }
}
