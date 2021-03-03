namespace StudentsSocialMedia.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Moq;
    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Web.ViewModels.Messages;
    using Xunit;

    public class MessagesServiceTests
    {
        [Fact]
        public async Task Create_Should_Work_Correctly()
        {
            List<Message> messages = new List<Message>();
            var mockedRepository = new Mock<IDeletableEntityRepository<Message>>();
            mockedRepository.Setup(x => x.AddAsync(It.IsAny<Message>())).Callback(
                (Message message) => messages.Add(message));
            IMessagesService messagesService = new MessagesService(mockedRepository.Object);
            CreateMessageInputModel inputFirst = new CreateMessageInputModel
            {
                Content = "Some content",
                UserId = "stefko",
                GroupId = "tennis",
            };
            CreateMessageInputModel inputSecond = new CreateMessageInputModel
            {
                Content = "Some content",
                UserId = "miro",
                GroupId = "programming",
            };
            CreateMessageInputModel inputThird = new CreateMessageInputModel
            {
                Content = "Some content",
                UserId = "gecata",
                GroupId = "gaming",
            };

            await messagesService.Create(inputFirst);
            await messagesService.Create(inputSecond);
            await messagesService.Create(inputThird);

            Assert.Equal(3, messages.Count());
        }

        [Theory]
        [InlineData("tennis")]
        public void GetAllById_Should_Return_Correct_Results(string id)
        {
            // Arrange
            List<Message> testData = this.GetTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Message>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IMessagesService messagesService = new MessagesService(mockedRepository.Object);
            IEnumerable<string> expectedUserIds = testData.Where(x => x.GroupId == id).OrderBy(x => x.CreatedOn).Select(x => x.UserId);
            IEnumerable<string> expectedContents = testData.Select(x => x.Content);
            IEnumerable<string> expectedUsernames = testData.Where(x => x.GroupId == id).OrderBy(x => x.CreatedOn).Select(x => x.User.UserName);

            // Act
            IEnumerable<AllMessagesByIdViewModel> actual = messagesService.GetAllById<AllMessagesByIdViewModel>(id);

            // Assert
            Assert.Equal(expectedUserIds, actual.Select(x => x.UserId));
            Assert.Equal(expectedContents, actual.Select(x => x.Content));
            Assert.Equal(expectedUsernames, actual.Select(x => x.UserUserName));
        }

        private List<Message> GetTestData()
        {
            List<Message> messages = new List<Message>()
            {
                new Message
                {
                    Id = "message1",
                    Content = "Some content",
                    CreatedOn = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Id = "stefko",
                        UserName = "stefkonoisyboy",
                        Images = new HashSet<Image>()
                        {
                            new Image
                            {
                                Id = "image1",
                                Extension = ".jpg",
                                RemoteImageUrl = "sdgdfgfhf",
                                CreatedOn = DateTime.UtcNow,
                            },
                        },
                    },
                    Group = new Group
                    {
                        Id = "tennis",
                    },
                    GroupId = "tennis",
                    UserId = "stefko",
                },
                new Message
                {
                    Content = "Some content",
                    CreatedOn = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Id = "miro",
                        UserName = "ghostmatser",
                        Images = new HashSet<Image>()
                        {
                            new Image
                            {
                                Id = "image2",
                                Extension = ".jpg",
                            },
                        },
                    },
                    GroupId = "tennis",
                    UserId = "miro",
                },
                new Message
                {
                    Content = "Some content",
                    CreatedOn = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Id = "gecata",
                        UserName = "gecata234354",
                        Images = new HashSet<Image>()
                        {
                            new Image
                            {
                                Id = "image3",
                                Extension = ".jpg",
                            },
                        },
                    },
                    GroupId = "tennis",
                    UserId = "gecata",
                },
                new Message
                {
                    Content = "Some content",
                    CreatedOn = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Id = "kenny",
                        UserName = "kennyS",
                        Images = new HashSet<Image>()
                        {
                            new Image
                            {
                                Id = "image4",
                                Extension = ".jpg",
                            },
                        },
                    },
                    Group = new Group
                    {
                        Id = "gaming",
                    },
                    GroupId = "gaming",
                    UserId = "kenny",
                },
                new Message
                {
                    Content = "Some content",
                    CreatedOn = DateTime.UtcNow,
                    User = new ApplicationUser
                    {
                        Id = "Djokovic",
                        UserName = "Djoker_Nole",
                        Images = new HashSet<Image>()
                        {
                            new Image
                            {
                                Id = "image5",
                                Extension = ".jpg",
                            },
                        },
                    },
                    GroupId = "tennis",
                    UserId = "Djokovic",
                },
            };

            return messages;
        }
    }
}
