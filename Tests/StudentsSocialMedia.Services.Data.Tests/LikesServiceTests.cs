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
    using StudentsSocialMedia.Web.ViewModels.Likes;
    using Xunit;

    public class LikesServiceTests
    {
        [Theory]
        [InlineData("2")]
        public void GetAllById_Should_Return_Correct_Results(string likeId)
        {
            //Arrange
            List<Like> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Like>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ILikesService likeService = new LikesService(mockedRepository.Object);
            IEnumerable<string> expectedIds = testData.Select(x => x.Id).ToList();
            IEnumerable<string> expectedCreatorIds = testData.Select(x => x.CreatorId).ToList();
            IEnumerable<string> expectedPostIds = testData.Select(x => x.PostId).ToList();

            //Act
            IEnumerable<AllLikesByIdViewModel> actual = likeService.GetAllById<AllLikesByIdViewModel>(likeId);

            //Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedCreatorIds, actual.Select(x => x.CreatorId));
            Assert.Equal(expectedPostIds, actual.Select(x => x.PostId));
        }

        private List<Like> GetTestData()
        {
            List<Like> likes = new List<Like>()
            {
                new Like
                {
                    Id = "1",
                    CreatorId = "2",
                    Creator = new ApplicationUser
                    {
                        UserName = "Ivan",
                    },
                    PostId = "2",
                    Post = new Post
                    {
                        Content = "Post content",
                    },
                },
                new Like
                {
                    Id = "2",
                    CreatorId = "3",
                    Creator = new ApplicationUser
                    {
                        UserName = "Mihail",
                    },
                    PostId = "1",
                    Post = new Post
                    {
                        Content = "A lot of content",
                    },
                },
            };
            return likes;
        }
    }
}
