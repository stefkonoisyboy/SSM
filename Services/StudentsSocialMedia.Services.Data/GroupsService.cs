namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Groups;
    using StudentsSocialMedia.Web.ViewModels.SearchGroups;

    public class GroupsService : IGroupsService
    {
        private readonly IDeletableEntityRepository<Group> groupsRepository;
        private readonly IDeletableEntityRepository<MemberGroup> memberGroupsRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public GroupsService(
            IDeletableEntityRepository<Group> groupsRepository,
            IDeletableEntityRepository<MemberGroup> memberGroupsRepository,
            IDeletableEntityRepository<Image> imagesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.groupsRepository = groupsRepository;
            this.memberGroupsRepository = memberGroupsRepository;
            this.imagesRepository = imagesRepository;
            this.usersRepository = usersRepository;
        }

        public async Task Create(CreateGroupInputModel input, string path)
        {
            Group group = new Group
            {
                Name = input.Name,
                Description = input.Description,
                CreatorId = input.CreatorId,
                SubjectId = input.SubjectId,
            };

            Image image = new Image
            {
                Id = "group" + Guid.NewGuid().ToString(),
                Extension = Path.GetExtension(input.Image.FileName),
                GroupId = group.Id,
            };

            string physicalPath = $"{path}/images/groups/{image.Id}{image.Extension}";
            using Stream stream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(stream);

            MemberGroup memberGroup = new MemberGroup
            {
                MemberId = input.CreatorId,
                GroupId = group.Id,
            };

            await this.groupsRepository.AddAsync(group);
            await this.imagesRepository.AddAsync(image);
            await this.memberGroupsRepository.AddAsync(memberGroup);

            await this.groupsRepository.SaveChangesAsync();
            await this.imagesRepository.SaveChangesAsync();
            await this.memberGroupsRepository.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            Group group = this.groupsRepository.All().FirstOrDefault(g => g.Id == id);
            this.groupsRepository.Delete(group);
            await this.groupsRepository.SaveChangesAsync();
        }

        public async Task Update(string id, EditGroupInputModel input)
        {
            Group group = this.groupsRepository.All().FirstOrDefault(g => g.Id == id);
            group.Name = input.Name;
            group.Description = input.Description;
            group.SubjectId = input.SubjectId;
            await this.groupsRepository.SaveChangesAsync();
        }

        public async Task Follow(string groupId, string userId)
        {
            MemberGroup memberGroup = new MemberGroup
            {
                GroupId = groupId,
                MemberId = userId,
            };

            await this.memberGroupsRepository.AddAsync(memberGroup);
            await this.memberGroupsRepository.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetAllAsListItems(string id)
        {
            IEnumerable<SelectListItem> groupsItems = this.groupsRepository
                .All()
                .Where(g => g.CreatorId == id)
                .Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.Id,
                })
                .ToList();

            return groupsItems;
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> groups = this.memberGroupsRepository
                .All()
                .OrderByDescending(g => g.CreatedOn)
                .Where(g => g.MemberId == id && !g.Group.IsDeleted)
                .To<T>()
                .ToList();

            return groups;
        }

        public IEnumerable<T> GetAllBySubjects<T>(IEnumerable<string> subjects, string id)
        {
            IEnumerable<T> groups = this.groupsRepository
                .All()
                .OrderByDescending(g => g.CreatedOn)
                .Where(g => subjects.Contains(g.Subject.Name) && !g.Members.Any(m => m.MemberId == id))
                .To<T>()
                .ToList();

            return groups;
        }

        public async Task Unfollow(string groupId, string userId)
        {
            MemberGroup memberGroup = this.memberGroupsRepository
                .All()
                .FirstOrDefault(mg => mg.GroupId == groupId && mg.MemberId == userId);

            this.memberGroupsRepository.Delete(memberGroup);
            await this.memberGroupsRepository.SaveChangesAsync();
        }

        public T GetById<T>(string id)
        {
            T group = this.groupsRepository
                .All()
                .Where(g => g.Id == id)
                .To<T>()
                .FirstOrDefault();

            return group;
        }

        public IEnumerable<T> GetAllMembersById<T>(string id)
        {
            IEnumerable<T> members = this.memberGroupsRepository
                .All()
                .Where(mg => mg.GroupId == id)
                .To<T>()
                .ToList();

            return members;
        }

        public IEnumerable<T> GetAllByNameAndSubjects<T>(SearchInputModel input)
        {
            var query = this.groupsRepository.All().AsQueryable();

            if (input.Name != null)
            {
                query = query.Where(g => g.Name.Contains(input.Name));
            }

            query = query.Where(g => g.SubjectId == input.SubjectId);

            return query.To<T>().ToList();
        }
    }
}
