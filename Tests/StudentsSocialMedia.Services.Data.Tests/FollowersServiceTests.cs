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
    using StudentsSocialMedia.Web.ViewModels.Followers;
    using Xunit;

    public class FollowersServiceTests
    {
        [Theory]
        [InlineData("1")]
        public void GetAllById_Should_Return_Correct_Results(string followerId)
        {
            //Arrange
            List<Follower> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Follower>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IFollowersService followersService = new FollowersService(mockedRepository.Object);
            IEnumerable<string> expectedIds = testData.Select(x => x.FollowersId).ToList();
            IEnumerable<string> expectedUserNames = testData.Select(x => x.Followers.UserName).ToList();
            IEnumerable<string> expectedTownNames = testData.Select(x => x.Followers.Town.Name).ToList();

            //Act
            IEnumerable<AllFollowersViewModel> actual = followersService.GetAllById<AllFollowersViewModel>(followerId);

            //Assert
            Assert.Equal(expectedIds, actual.Select(x => x.FollowersId));
            Assert.Equal(expectedUserNames, actual.Select(x => x.FollowersUserName));
            Assert.Equal(expectedTownNames, actual.Select(x => x.FollowersTownName));

        }

        private List<Follower> GetTestData()
        {
            List<Follower> followers = new List<Follower>()
            {
                new Follower
                {
                    Id = "1",
                    FollowersId = "1",
                    Followers = new ApplicationUser
                    {
                        UserName = "Nelly",
                        Town = new Town
                        {
                            Name = "Smolyan",
                        },
                        Images = new List<Image>()
                        {
                            new Image
                            {
                                Id = "imageOfAuthor",
                                Extension = ".jpg",
                            },
                        },
                    },
                    Following = new ApplicationUser
                    {
                        UserName = "testUser7",
                    },
                },
                new Follower
                {
                    Id = "2",
                    FollowersId = "2",
                    Followers = new ApplicationUser
                    {
                        UserName = "testUser1",
                        Town = new Town
                        {
                            Name = "Sofia",
                        },
                        Images = new List<Image>()
                        {
                            new Image
                            {
                                Id = "authorImage",
                                Extension = ".jpg",
                            },
                        },
                    },
                    Following = new ApplicationUser
                    {
                        UserName = "testUser2",
                    },
                },
            };
            return followers;
        }
    }
}
