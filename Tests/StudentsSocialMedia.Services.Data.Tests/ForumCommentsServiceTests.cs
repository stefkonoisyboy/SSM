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
    using StudentsSocialMedia.Web.ViewModels.Forum.Comments;
    using Xunit;

    public class ForumCommentsServiceTests
    {
        [Fact]
        public async Task Create_Should_Work_Correctly()
        {
            // Arrange
            List<ForumComment> list = new List<ForumComment>();
            var mockedRepository = new Mock<IDeletableEntityRepository<ForumComment>>();
            mockedRepository.Setup(x => x.AddAsync(It.IsAny<ForumComment>())).Callback(
                (ForumComment forumComment) => list.Add(forumComment));
            IForumCommentsService forumCommentsService = new ForumCommentsService(mockedRepository.Object);
            CreateCommentInputModel inputFirst = new CreateCommentInputModel
            {
                Content = "Some content",
                UserId = "stefko",
                PostId = "1",
            };
            CreateCommentInputModel inputSecond = new CreateCommentInputModel
            {
                Content = "Some content",
                UserId = "miro",
                PostId = "2",
            };

            // Act
            await forumCommentsService.Create(inputFirst);
            await forumCommentsService.Create(inputSecond);

            // Assert
            Assert.Equal(2, list.Count());
        }

        [Theory]
        [InlineData("post1")]
        public void GetAllById_Should_Return_Correct_Results(string postId)
        {
            // Arrange
            List<ForumComment> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ForumComment>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IForumCommentsService forumCommentsService = new ForumCommentsService(mockedRepository.Object);
            IEnumerable<string> expectedContents = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Content);
            IEnumerable<string> expectedUsernames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.User.UserName);
            IEnumerable<string> expectedUserIds = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.UserId);

            // Act
            IEnumerable<AllCommentsByIdViewModel> actual = forumCommentsService.GetAllById<AllCommentsByIdViewModel>(postId);

            // Assert
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
            Assert.Equal(expectedUserIds, actual.Select(x => x.UserId));
            Assert.Equal(expectedUsernames, actual.Select(x => x.UserUserName));
        }

        private List<ForumComment> GetTestData()
        {
            List<ForumComment> forumComments = new List<ForumComment>()
            {
                new ForumComment
                {
                    Id = "1",
                    User = new ApplicationUser
                    {
                        Id = "stefko",
                        UserName = "stefkonoisyboy",
                    },
                    CreatedOn = DateTime.UtcNow,
                    PostId = "post1",
                },
                new ForumComment
                {
                    Id = "2",
                    User = new ApplicationUser
                    {
                        Id = "miro",
                        UserName = "ghost_master",
                    },
                    CreatedOn = DateTime.UtcNow,
                    PostId = "post1",
                },
                new ForumComment
                {
                    Id = "3",
                    User = new ApplicationUser
                    {
                        Id = "gecata",
                        UserName = "gecata12132",
                    },
                    CreatedOn = DateTime.UtcNow,
                    PostId = "post2",
                },
            };

            return forumComments;
        }
    }
}
