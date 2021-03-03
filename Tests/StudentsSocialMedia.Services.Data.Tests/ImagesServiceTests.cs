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
    using StudentsSocialMedia.Web.ViewModels.Images;
    using Xunit;

    public class ImagesServiceTests
    {
        [Theory]
        [InlineData("1")]
        [InlineData("3")]
        public void GetAllById_Should_Return_Correct_Results(string imageId)
        {
            //Arrange
            List<Image> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Image>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IImagesService imagesService = new ImagesService(mockedRepository.Object);
            IEnumerable<string> expectedIds = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Id);
            IEnumerable<string> expectedExtentionss = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Extension);
            //Act
            IEnumerable<AllImagesViewModel> actual = imagesService.GetAllById<AllImagesViewModel>(imageId);

            //Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedExtentionss, actual.Select(x => x.Extension));
        }

        private List<Image> GetTestData()
        {
            List<Image> images = new List<Image>()
            {
                new Image
                {
                    Id = "1",
                    Extension = ".jpg",
                    CreatedOn = DateTime.UtcNow,
                },
                new Image
                {
                    Id = "4",
                    Extension = ".jpg",
                    CreatedOn = DateTime.UtcNow,
                },
                new Image
                {
                    Id = "3",
                    Extension = ".jpg",
                    CreatedOn = DateTime.UtcNow,
                },
            };
            return images;
        }
    }
}
