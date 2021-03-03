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
    using StudentsSocialMedia.Web.ViewModels.Replies;
    using Xunit;

    public class RepliesServiceTests
    {
        public void GetAllById_Should_Return_Correct_Results(string replyId)
        {
            //Arrange
            List<Reply> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Reply>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IRepliesService repliesService = new RepliesService(mockedRepository.Object);
            IEnumerable<string> expectedContents = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Content);
            IEnumerable<string> expectedAuthorIds = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.AuthorId);
            IEnumerable<string> expectedAuthorUserNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Author.UserName);

            //Act
            IEnumerable<AllRepliesViewModel> actual = repliesService.GetAll<AllRepliesViewModel>(replyId);

            //Assert
            Assert.Equal(expectedAuthorIds, actual.Select(x => x.AuthorId));
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
            Assert.Equal(expectedAuthorUserNames, actual.Select(x => x.AuthorUserName));
        }

        private List<Reply> GetTestData()
        {
            List<Reply> replies = new List<Reply>()
            {
                new Reply
                {
                    Id = "1",
                    Content = "Reply content",
                    AuthorId = "2",
                    Author = new ApplicationUser
                    {
                        UserName = "testUser1",
                        Images = new List<Image>()
                        {
                            new Image
                            {
                                Id = "imageOfAuthor",
                                Extension = ".jpg",
                            },
                        },
                    },
                    CreatedOn = DateTime.UtcNow,
                },
                new Reply
                {
                    Id = "2",
                    Content = "A lot of content",
                    AuthorId = "3",
                    Author = new ApplicationUser
                    {
                        UserName = "testUser2",
                        Images = new List<Image>()
                        {
                            new Image
                            {
                                Id = "image",
                                Extension = ".jpg",
                            },
                        },
                    },
                    CreatedOn = DateTime.UtcNow,
                },
            };
            return replies;
        }
    }
}
