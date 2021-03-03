namespace StudentsSocialMedia.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Moq;
    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Web.ViewModels.Groups;
    using StudentsSocialMedia.Web.ViewModels.SearchGroups;
    using Xunit;

    public class GroupsServiceTests
    {
        [Fact]
        public async Task Create_Should_Work_Correctly()
        {
            // Arrange
            List<Group> groups = new List<Group>();
            List<Stream> streams = new List<Stream>();
            List<MemberGroup> memberGroups = new List<MemberGroup>();
            List<Image> images = new List<Image>();
            var mockedGroupsRepository = new Mock<IDeletableEntityRepository<Group>>();
            var mockedMemberGroupsRepository = new Mock<IDeletableEntityRepository<MemberGroup>>();
            var mockedImagesRepository = new Mock<IDeletableEntityRepository<Image>>();
            var mockedImage = new Mock<IFormFile>();
            mockedImage.Setup(x => x.FileName).Returns("image.jpg");
            mockedImage.Setup(x => x.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Callback(
                (Stream stream, CancellationToken token) => streams.Add(stream));
            mockedGroupsRepository.Setup(x => x.AddAsync(It.IsAny<Group>())).Callback(
                (Group group) => groups.Add(group));
            mockedMemberGroupsRepository.Setup(x => x.AddAsync(It.IsAny<MemberGroup>())).Callback(
                (MemberGroup memberGroup) => memberGroups.Add(memberGroup));
            mockedImagesRepository.Setup(x => x.AddAsync(It.IsAny<Image>())).Callback(
                (Image image) => images.Add(image));
            IGroupsService groupsService = new GroupsService(mockedGroupsRepository.Object, mockedMemberGroupsRepository.Object, mockedImagesRepository.Object, null);
            CreateGroupInputModel input = new CreateGroupInputModel
            {
                Name = "Tennis",
                CreatorId = "stefko",
                Description = "Some description",
                Image = mockedImage.Object,
            };

            // Act
            await groupsService.Create(input, "example");

            // Assert
            Assert.Equal(1, groups.Count());
            Assert.Equal(1, memberGroups.Count());
            Assert.Equal(1, images.Count());
        }

        [Theory]
        [InlineData("tennis")]
        public async Task Delete_Should_Work_Correctly(string id)
        {
            // Arrange
            List<Group> groups = this.GetGroupsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Group>>();
            mockedRepository.Setup(x => x.All()).Returns(groups.AsQueryable());
            mockedRepository.Setup(x => x.Delete(It.IsAny<Group>())).Callback(
                (Group group) => groups.Remove(group));
            IGroupsService groupsService = new GroupsService(mockedRepository.Object, null, null, null);

            // Act
            await groupsService.Delete(id);

            // Assert
            Assert.Equal(1, groups.Count());
            Assert.Null(groups.FirstOrDefault(x => x.Id == id));
        }

        [Theory]
        [InlineData("tennis")]
        public async Task Update_Should_Work_Correctly(string id)
        {
            // Arrange
            List<Group> groups = this.GetGroupsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Group>>();
            mockedRepository.Setup(x => x.All()).Returns(groups.AsQueryable());
            IGroupsService groupsService = new GroupsService(mockedRepository.Object, null, null, null);
            EditGroupInputModel editModel = new EditGroupInputModel
            {
                Id = id,
                Name = "Tennis forever",
                Description = "Some description",
                CreatorId = "stefko",
                SubjectId = "P.E.",
            };

            // Act
            await groupsService.Update(id, editModel);
            Group group = groups.FirstOrDefault(x => x.Id == id);

            // Assert
            Assert.Equal(editModel.Name, group.Name);
            Assert.Equal(editModel.Description, group.Description);
            Assert.Equal(editModel.SubjectId, group.SubjectId);
        }

        [Theory]
        [InlineData("tennis", "stefko")]
        public async Task Follow_Should_Work_Correctly(string groupId, string userId)
        {
            // Arrange
            List<MemberGroup> list = new List<MemberGroup>();
            var mockedRepository = new Mock<IDeletableEntityRepository<MemberGroup>>();
            mockedRepository.Setup(x => x.AddAsync(It.IsAny<MemberGroup>())).Callback(
                (MemberGroup memberGroup) => list.Add(memberGroup));
            IGroupsService groupsService = new GroupsService(null, mockedRepository.Object, null, null);

            // Act
            await groupsService.Follow(groupId, userId);

            // Assert
            Assert.Equal(1, list.Count());
        }

        [Theory]
        [InlineData("tennis", "stefko")]
        public async Task Unfollow_Should_Work_Correctly(string groupId, string userId)
        {
            // Arrange
            List<MemberGroup> list = this.GetMemberGroupsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MemberGroup>>();
            mockedRepository.Setup(x => x.All()).Returns(list.AsQueryable());
            mockedRepository.Setup(x => x.Delete(It.IsAny<MemberGroup>())).Callback(
                (MemberGroup memberGroup) => list.Remove(memberGroup));
            IGroupsService groupsService = new GroupsService(null, mockedRepository.Object, null, null);

            // Act
            await groupsService.Unfollow(groupId, userId);

            // Assert
            Assert.Equal(2, list.Count());
        }

        [Theory]
        [InlineData("stefko")]
        public void GetAllAsListItems_Should_Work_Correctly(string creatorId)
        {
            // Arrange
            List<Group> testData = this.GetGroupsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Group>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IGroupsService groupsService = new GroupsService(mockedRepository.Object, null, null, null);
            IEnumerable<string> expectedIds = testData.Where(x => x.CreatorId == creatorId).Select(x => x.Id);
            IEnumerable<string> expectedNames = testData.Where(x => x.CreatorId == creatorId).Select(x => x.Name);

            // Act
            IEnumerable<SelectListItem> actual = groupsService.GetAllAsListItems(creatorId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Value));
            Assert.Equal(expectedNames, actual.Select(x => x.Text));
        }

        [Theory]
        [InlineData("stefko")]
        public void GetAllById_Should_Work_Correctly(string memberId)
        {
            // Arrange
            List<MemberGroup> testData = this.GetMemberGroupsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MemberGroup>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IGroupsService groupsService = new GroupsService(null, mockedRepository.Object, null, null);
            IEnumerable<string> expectedIds = testData.Where(x => x.MemberId == memberId && !x.IsDeleted).OrderByDescending(x => x.CreatedOn).Select(x => x.GroupId);
            IEnumerable<string> expectedNames = testData.Where(x => x.MemberId == memberId && !x.IsDeleted).OrderByDescending(x => x.CreatedOn).Select(x => x.Group.Name);
            IEnumerable<string> expectedUsernames = testData.Where(x => x.MemberId == memberId && !x.IsDeleted).OrderByDescending(x => x.CreatedOn).Select(x => x.Group.Creator.UserName);
            IEnumerable<int> expectedCounts = testData.Where(x => x.MemberId == memberId && !x.IsDeleted).OrderByDescending(x => x.CreatedOn).Select(x => x.Group.Members.Count());

            // Act
            IEnumerable<AllGroupsByIdViewModel> actual = groupsService.GetAllById<AllGroupsByIdViewModel>(memberId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.GroupId));
            Assert.Equal(expectedNames, actual.Select(x => x.GroupName));
            Assert.Equal(expectedUsernames, actual.Select(x => x.GroupCreatorUserName));
            Assert.Equal(expectedCounts, actual.Select(x => x.GroupMembersCount));
        }

        [Theory]
        [InlineData("stefko")]
        public void GetAllBySubjects_Should_Work_Correctly(string memberId)
        {
            // Arrange
            List<Group> testData = this.GetGroupsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Group>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IGroupsService groupsService = new GroupsService(mockedRepository.Object, null, null, null);
            IEnumerable<string> subjects = new List<string>() { "P.E.", "Informatics" };
            IEnumerable<string> expectedIds = testData.OrderByDescending(g => g.CreatedOn).Where(g => subjects.Contains(g.Subject.Name) && !g.Members.Any(m => m.MemberId == memberId)).Select(g => g.Id);
            IEnumerable<string> expectedNames = testData.OrderByDescending(g => g.CreatedOn).Where(g => subjects.Contains(g.Subject.Name) && !g.Members.Any(m => m.MemberId == memberId)).Select(g => g.Name);
            IEnumerable<string> expectedUsernames = testData.OrderByDescending(g => g.CreatedOn).Where(g => subjects.Contains(g.Subject.Name) && !g.Members.Any(m => m.MemberId == memberId)).Select(g => g.Creator.UserName);
            IEnumerable<int> expectedCounts = testData.OrderByDescending(g => g.CreatedOn).Where(g => subjects.Contains(g.Subject.Name) && !g.Members.Any(m => m.MemberId == memberId)).Select(g => g.Members.Count());

            // Act
            IEnumerable<AllSuggestedGroupsByIdViewModel> actual = groupsService.GetAllBySubjects<AllSuggestedGroupsByIdViewModel>(subjects, memberId);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedUsernames, actual.Select(x => x.CreatorUserName));
            Assert.Equal(expectedCounts, actual.Select(x => x.MembersCount));
        }

        [Theory]
        [InlineData("tennis")]
        public void GetById_Should_Work_Correctly(string id)
        {
            // Arrange
            List<Group> testData = this.GetGroupsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Group>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IGroupsService groupsService = new GroupsService(mockedRepository.Object, null, null, null);
            string expectedId = "tennis";
            string expectedName = "Tennis forever";
            string expectedUsername = "stefkonoisyboy";
            int expectedMembersCount = 1;

            // Act
            ActivityByIdViewModel actual = groupsService.GetById<ActivityByIdViewModel>(id);

            // Assert
            Assert.Equal(expectedId, actual.Id);
            Assert.Equal(expectedName, actual.Name);
            Assert.Equal(expectedUsername, actual.CreatorUserName);
            Assert.Equal(expectedMembersCount, actual.MembersCount);
        }

        [Theory]
        [InlineData("tennis")]
        public void GetAllMembersById_Should_Work_Correctly(string id)
        {
            // Arrange
            List<MemberGroup> testData = this.GetMemberGroupsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<MemberGroup>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            IGroupsService groupsService = new GroupsService(null, mockedRepository.Object, null, null);
            IEnumerable<string> expectedIds = testData.Where(x => x.GroupId == id).Select(x => x.MemberId);
            IEnumerable<string> expectedTowns = testData.Where(x => x.GroupId == id).Select(x => x.Member.Town.Name);
            IEnumerable<string> expectedUsernames = testData.Where(x => x.GroupId == id).Select(x => x.Member.UserName);
            IEnumerable<int> expectedFollowersCount = testData.Where(x => x.GroupId == id).Select(x => x.Member.Followers.Count());
            IEnumerable<int> expectedFollowingsCount = testData.Where(x => x.GroupId == id).Select(x => x.Member.Followings.Count());
            IEnumerable<int> expectedImagesCount = testData.Where(x => x.GroupId == id).Select(x => x.Member.Images.Count());

            // Act
            IEnumerable<AllMembersByIdViewModel> actual = groupsService.GetAllMembersById<AllMembersByIdViewModel>(id);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.MemberId));
            Assert.Equal(expectedTowns, actual.Select(x => x.MemberTownName));
            Assert.Equal(expectedUsernames, actual.Select(x => x.MemberUserName));
            Assert.Equal(expectedFollowersCount, actual.Select(x => x.MemberFollowersCount));
            Assert.Equal(expectedFollowingsCount, actual.Select(x => x.MemberFollowingsCount));
            Assert.Equal(expectedImagesCount, actual.Select(x => x.MemberImagesCount));
        }

        [Fact]
        public void GetAllByNameAndSubjects_Should_Work_Correctly()
        {
            // Arrange
            List<Group> testData = this.GetGroupsTestData();
            var mockedRepository = new Mock<IDeletableEntityRepository<Group>>();
            mockedRepository.Setup(x => x.All()).Returns(testData.AsQueryable());
            SearchInputModel input = new SearchInputModel
            {
                Name = "te",
                SubjectId = "1",
            };
            IGroupsService groupsService = new GroupsService(mockedRepository.Object, null, null, null);
            IEnumerable<string> expectedIds = testData.OrderByDescending(g => g.CreatedOn).Where(g => g.Subject.Name.Contains(input.Name) && g.SubjectId == input.SubjectId).Select(g => g.Id);
            IEnumerable<string> expectedNames = testData.OrderByDescending(g => g.CreatedOn).Where(g => g.Subject.Name.Contains(input.Name) && g.SubjectId == input.SubjectId).Select(g => g.Name);
            IEnumerable<string> expectedUsernames = testData.OrderByDescending(g => g.CreatedOn).Where(g => g.Subject.Name.Contains(input.Name) && g.SubjectId == input.SubjectId).Select(g => g.Creator.UserName);
            IEnumerable<int> expectedCounts = testData.OrderByDescending(g => g.CreatedOn).Where(g => g.Subject.Name.Contains(input.Name) && g.SubjectId == input.SubjectId).Select(g => g.Members.Count());

            // Act
            IEnumerable<AllSuggestedGroupsByIdViewModel> actual = groupsService.GetAllByNameAndSubjects<AllSuggestedGroupsByIdViewModel>(input);

            // Assert
            Assert.Equal(expectedIds, actual.Select(x => x.Id));
            Assert.Equal(expectedNames, actual.Select(x => x.Name));
            Assert.Equal(expectedUsernames, actual.Select(x => x.CreatorUserName));
            Assert.Equal(expectedCounts, actual.Select(x => x.MembersCount));
        }

        private List<Group> GetGroupsTestData()
        {
            List<Group> groups = new List<Group>()
            {
                new Group
                {
                    Id = "tennis",
                    Name = "Tennis forever",
                    CreatedOn = DateTime.UtcNow,
                    Creator = new ApplicationUser
                    {
                        Id = "stefko",
                        UserName = "stefkonoisyboy",
                    },
                    CreatorId = "stefko",
                    Members = new HashSet<MemberGroup>()
                    {
                        new MemberGroup
                        {
                            MemberId = "stefko",
                            GroupId = "tennis",
                        },
                    },
                    Images = new HashSet<Image>()
                    {
                        new Image
                        {
                            Id = "group_image1",
                            Extension = ".jpg",
                            CreatedOn = DateTime.UtcNow,
                        },
                    },
                    Subject = new Subject
                    {
                        Id = "1",
                        Name = "P.E.",
                    },
                    SubjectId = "1",
                },
                new Group
                {
                    Id = "programming",
                    Name = "Programming forever",
                    CreatedOn = DateTime.UtcNow,
                    Creator = new ApplicationUser
                    {
                        Id = "miro",
                        UserName = "GhostMaster",
                    },
                    CreatorId = "miro",
                    Members = new HashSet<MemberGroup>()
                    {
                        new MemberGroup
                        {
                            MemberId = "miro",
                            GroupId = "programming",
                        },
                    },
                    Images = new HashSet<Image>()
                    {
                        new Image
                        {
                            Id = "group_image2",
                            Extension = ".jpg",
                            CreatedOn = DateTime.UtcNow,
                        },
                    },
                    Subject = new Subject
                    {
                        Id = "2",
                        Name = "Informatics",
                    },
                    SubjectId = "2",
                },
            };

            return groups;
        }

        private List<MemberGroup> GetMemberGroupsTestData()
        {
            return new List<MemberGroup>()
            {
                new MemberGroup
                {
                    Id = "1",
                    CreatedOn = DateTime.UtcNow,
                    Member = new ApplicationUser
                    {
                        Id = "stefko",
                        UserName = "stefkonoisyboy",
                        Town = new Town
                        {
                            Id = "1",
                            Name = "Smolyan",
                        },
                        Images = new HashSet<Image>(),
                        Followers = new HashSet<Follower>(),
                        Followings = new HashSet<Follower>(),
                    },
                    Group = new Group
                    {
                        Id = "tennis",
                        Name = "Tennis forever",
                        Creator = new ApplicationUser
                        {
                            Id = "teacher1",
                            UserName = "fgfghbfdbfg",
                        },
                        Images = new HashSet<Image>()
                        {
                            new Image
                            {
                                Id = "group_iamge1",
                                Extension = ".jpg",
                                RemoteImageUrl = "example",
                                CreatedOn = DateTime.UtcNow,
                            },
                        },
                        Members = new HashSet<MemberGroup>(),
                    },
                    MemberId = "stefko",
                    GroupId = "tennis",
                },
                new MemberGroup
                {
                    Id = "2",
                    CreatedOn = DateTime.UtcNow,
                    Member = new ApplicationUser
                    {
                        Id = "stefko",
                        UserName = "stefkonoisyboy",
                        TownId = "1",
                        Images = new HashSet<Image>(),
                        Followers = new HashSet<Follower>(),
                        Followings = new HashSet<Follower>(),
                    },
                    Group = new Group
                    {
                        Id = "programming",
                        Name = "Programming forever",
                        Creator = new ApplicationUser
                        {
                            Id = "teacher2",
                            UserName = "fgfghbfdbffsdfg",
                        },
                        Images = new HashSet<Image>()
                        {
                            new Image
                            {
                                Id = "group_iamge2",
                                Extension = ".jpg",
                            },
                        },
                        Members = new HashSet<MemberGroup>(),
                    },
                    MemberId = "stefko",
                    GroupId = "programming",
                },
                new MemberGroup
                {
                    Id = "3",
                    CreatedOn = DateTime.UtcNow,
                    Member = new ApplicationUser
                    {
                        Id = "miro",
                        UserName = "GhostMaster",
                        TownId = "1",
                        Images = new HashSet<Image>(),
                        Followers = new HashSet<Follower>(),
                        Followings = new HashSet<Follower>(),
                    },
                    Group = new Group
                    {
                        Id = "gaming",
                        Name = "Gaming forever",
                        Creator = new ApplicationUser
                        {
                            Id = "teacher3",
                            UserName = "fgsdfsfghbfdbfg",
                        },
                        Images = new HashSet<Image>()
                        {
                            new Image
                            {
                                Id = "group_iamge3",
                                Extension = ".jpg",
                            },
                        },
                        Members = new HashSet<MemberGroup>(),
                    },
                    MemberId = "miro",
                    GroupId = "gaming",
                },
            };
        }
    }
}
