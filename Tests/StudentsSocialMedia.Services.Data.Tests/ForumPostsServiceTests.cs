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
    using StudentsSocialMedia.Web.ViewModels.Forum.Posts;
    using Xunit;

    public class ForumPostsServiceTests
    {
        [Fact]
        public async Task Create_Should_Work_Correctly()
        {
            // Arrange
            List<ForumPost> list = new List<ForumPost>();
            var mockedRepository = new Mock<IDeletableEntityRepository<ForumPost>>();
            mockedRepository.Setup(x => x.AddAsync(It.IsAny<ForumPost>())).Callback(
                (ForumPost forumPost) => list.Add(forumPost));
            IForumPostsService forumPostsService = new ForumPostsService(mockedRepository.Object);
            CreatePostInputModel inputFirst = new CreatePostInputModel
            {
                Title = "Some title",
                CategoryId = "1",
                Content = "Some content",
                UserId = "stefko",
            };
            CreatePostInputModel inputSecond = new CreatePostInputModel
            {
                Title = "Some title",
                CategoryId = "2",
                Content = "Some content",
                UserId = "miro",
            };

            // Act
            await forumPostsService.Create(inputFirst);
            await forumPostsService.Create(inputSecond);

            // Assert
            Assert.Equal(2, list.Count());
        }

        [Theory]
        [InlineData("category1")]
        public void GetAllById_Should_Return_Correct_Results(string categoryId)
        {
            // Arrange
            List<ForumPost> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ForumPost>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IForumPostsService forumPostsService = new ForumPostsService(mockedRepository.Object);
            IEnumerable<string> expectedIds = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Id);
            IEnumerable<string> expectedTitles = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Title);
            IEnumerable<string> expectedContents = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Content);
            IEnumerable<string> expectedUsernames = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.User.UserName);
            IEnumerable<string> expectedUserIds = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.UserId);
            IEnumerable<string> expectedCategoriesTitles = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Category.Title);
            IEnumerable<int> expectedCommentsCount = testData.OrderByDescending(x => x.CreatedOn).Select(x => x.Comments.Count());

            // Act
            IEnumerable<AllPostsByIdViewModel> actual = forumPostsService.GetAllById<AllPostsByIdViewModel>(categoryId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedTitles, actual.Select(x => x.Title));
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
            Assert.Equal(expectedUsernames, actual.Select(x => x.UserUserName));
            Assert.Equal(expectedUserIds, actual.Select(x => x.UserId));
            Assert.Equal(expectedCategoriesTitles, actual.Select(x => x.CategoryTitle));
            Assert.Equal(expectedCommentsCount, actual.Select(x => x.CommentsCount));
        }

        private List<ForumPost> GetTestData()
        {
            List<ForumPost> forumPosts = new List<ForumPost>()
            {
                new ForumPost
                {
                    Id = "1",
                    Title = "Some title",
                    Content = "Some content",
                    User = new ApplicationUser
                    {
                        Id = "stefko",
                        UserName = "stefkonoisyboy",
                        Images = new List<Image>()
                        {
                            new Image
                            {
                                Id = "image1",
                                Extension = ".jpg",
                            },
                        },
                    },
                    CreatedOn = DateTime.UtcNow,
                    Category = new ForumCategory
                    {
                        Id = "category1",
                        Title = "Maths",
                    },
                    Comments = new List<ForumComment>()
                    {
                        new ForumComment
                        {
                            Id = "comment1",
                        },
                        new ForumComment
                        {
                            Id = "comment2",
                        },
                        new ForumComment
                        {
                            Id = "comment3",
                        },
                    },
                },
                new ForumPost
                {
                    Id = "2",
                    Title = "Some title",
                    Content = "Some content",
                    User = new ApplicationUser
                    {
                        Id = "miro",
                        UserName = "ghost_master",
                        Images = new List<Image>()
                        {
                            new Image
                            {
                                Id = "image2",
                                Extension = ".jpg",
                            },
                        },
                    },
                    CreatedOn = DateTime.UtcNow,
                    Category = new ForumCategory
                    {
                        Id = "category2",
                        Title = "Deutsch",
                    },
                    Comments = new List<ForumComment>()
                    {
                        new ForumComment
                        {
                            Id = "comment4",
                        },
                        new ForumComment
                        {
                            Id = "comment5",
                        },
                    },
                },
                new ForumPost
                {
                    Id = "3",
                    Title = "Some title",
                    Content = "Some content",
                    User = new ApplicationUser
                    {
                        Id = "gecata",
                        UserName = "gecata12234",
                        Images = new List<Image>()
                        {
                            new Image
                            {
                                Id = "image3",
                                Extension = ".jpg",
                            },
                        },
                    },
                    CreatedOn = DateTime.UtcNow,
                    Category = new ForumCategory
                    {
                        Id = "category1",
                        Title = "Maths",
                    },
                    Comments = new List<ForumComment>()
                    {
                        new ForumComment
                        {
                            Id = "comment6",
                        },
                    },
                },
            };

            return forumPosts;
        }
    }
}
