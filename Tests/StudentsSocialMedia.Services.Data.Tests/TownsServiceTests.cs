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
    using Xunit;

    public class TownsServiceTests
    {
        [Fact]
        public void GetAll_Should_Return_Correct_Results()
        {
            // Arrange
            List<Town> testData = this.GetTestData();
            var mockedRepo = new Mock<IDeletableEntityRepository<Town>>();
            mockedRepo.Setup(x => x.All()).Returns(testData.AsQueryable());
            ITownsService townsService = new TownsService(mockedRepo.Object);
            IEnumerable<string> expectedIds = testData.OrderBy(x => x.Name).Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.OrderBy(x => x.Name).Select(x => x.Name).ToList();

            // Act
            IEnumerable<SelectListItem> actualItems = townsService.GetAll();

            // Assert
            Assert.Equal(expectedIds, actualItems.Select(x => x.Value));
            Assert.Equal(expectedNames, actualItems.Select(x => x.Text));
        }

        private List<Town> GetTestData()
        {
            List<Town> towns = new List<Town>()
            {
                new Town
                {
                    Id = "1",
                    Name = "Smolyan",
                },
                new Town
                {
                    Id = "2",
                    Name = "Sofia",
                },
                new Town
                {
                    Id = "3",
                    Name = "Plovdiv",
                },
            };

            return towns;
        }
    }
}
