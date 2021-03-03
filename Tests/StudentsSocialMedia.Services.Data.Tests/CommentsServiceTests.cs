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
    using StudentsSocialMedia.Web.ViewModels.Comments;
    using StudentsSocialMedia.Web.ViewModels.Forum.Comments;
    using Xunit;

    public class CommentsServiceTests
    {
        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void GetAllById_Should_Return_Correct_Results(string postId)
        {
            //Arrange
            List<Comment> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Comment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICommentsService commentsService = new CommentsService(mockedRepository.Object);
            IEnumerable<string> expectedIds = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Id);
            IEnumerable<string> expectedAuthorIds = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.AuthorId);
            IEnumerable<string> expectedContents = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Content);
            IEnumerable<string> expectedUserNames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Author.UserName);

            //Act
            IEnumerable<AllViewModel> actual = commentsService.GetAll<AllViewModel>(postId);

            //Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedAuthorIds, actual.Select(x => x.AuthorId));
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
            Assert.Equal(expectedUserNames, actual.Select(x => x.AuthorUserName));
        }

        private List<Comment> GetTestData()
        {
            List<Comment> comments = new List<Comment>()
            {
                new Comment
                {
                    Id = "1",
                    Content = "Content for the comment",
                    AuthorId = "1",
                    Author = new ApplicationUser
                    {
                        UserName = "testUser5",
                        Images = new List<Image>()
                        {
                            new Image
                            {
                                Id = "imageOfAuthor",
                                Extension = ".jpg",
                            },
                        },
                    },
                },
                new Comment
                {
                    Id = "2",
                    Content = "Comment content",
                    AuthorId = "1",
                    Author = new ApplicationUser
                    {
                        UserName = "testUser5",
                        Images = new List<Image>()
                        {
                            new Image
                            {
                                Id = "imageOfAuthor",
                                Extension = ".jpg",
                            },
                        },
                    },
                },
            };
            return comments;
        }
    }
}
