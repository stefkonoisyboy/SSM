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
    using StudentsSocialMedia.Web.ViewModels.Forum.Categories;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public void GetAllAsItems_Should_Return_Correct_Results()
        {
            // Arrange
            List<ForumCategory> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ForumCategory>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICategoriesService categoriesService = new CategoriesService(mockedRepository.Object);
            IEnumerable<string> expectedIds = testData.Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Select(x => x.Title).ToList();

            // Act
            IEnumerable<SelectListItem> actual = categoriesService.GetAllAsSelectListItems();

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Value));
            Assert.Equal(expectedNames, actual.Select(x => x.Text));
        }

        [Fact]
        public void GetAll_Should_Return_Correct_Results()
        {
            // Arrange
            List<ForumCategory> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<ForumCategory>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            ICategoriesService categoriesService = new CategoriesService(mockedRepository.Object);
            IEnumerable<string> expectedIds = testData.Select(x => x.Id).ToList();
            IEnumerable<string> expectedNames = testData.Select(x => x.Title).ToList();
            IEnumerable<string> expectedUrls = testData.Select(x => x.RemoteImageUrl).ToList();
            IEnumerable<int> expectedPostsCount = testData.Select(x => x.Posts.Count()).ToList();

            // Act
            IEnumerable<AllCategoriesViewModel> actual = categoriesService.GetAll<AllCategoriesViewModel>();

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id).ToList());
            Assert.Equal(expectedNames, actual.Select(x => x.Title).ToList());
            Assert.Equal(expectedUrls, actual.Select(x => x.RemoteImageUrl).ToList());
            Assert.Equal(expectedPostsCount, actual.Select(x => x.PostsCount).ToList());
        }

        private List<ForumCategory> GetTestData()
        {
            List<ForumCategory> categories = new List<ForumCategory>()
            {
                new ForumCategory
                {
                    Id = "1",
                    Title = "Maths",
                    RemoteImageUrl = "some url",
                    Posts = new List<ForumPost>()
                    {
                        new ForumPost
                        {
                            Id = "post1",
                            CategoryId = "1",
                        },
                        new ForumPost
                        {
                            Id = "post2",
                            CategoryId = "1",
                        },
                        new ForumPost
                        {
                            Id = "post3",
                            CategoryId = "1",
                        },
                        new ForumPost
                        {
                            Id = "post4",
                            CategoryId = "1",
                        },
                    },
                },
                new ForumCategory
                {
                    Id = "2",
                    Title = "Bulgarian",
                    RemoteImageUrl = "some url",
                    Posts = new List<ForumPost>()
                    {
                        new ForumPost
                        {
                            Id = "post5",
                            CategoryId = "2",
                        },
                        new ForumPost
                        {
                            Id = "post6",
                            CategoryId = "2",
                        },
                        new ForumPost
                        {
                            Id = "post7",
                            CategoryId = "2",
                        },
                    },
                },
                new ForumCategory
                {
                    Id = "3",
                    Title = "Programming",
                    RemoteImageUrl = "some url",
                    Posts = new List<ForumPost>()
                    {
                        new ForumPost
                        {
                            Id = "post8",
                            CategoryId = "3",
                        },
                        new ForumPost
                        {
                            Id = "post9",
                            CategoryId = "3",
                        },
                    },
                },
            };

            return categories;
        }
    }
}
