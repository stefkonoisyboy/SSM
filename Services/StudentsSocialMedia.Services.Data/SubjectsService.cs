namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Subjects;

    public class SubjectsService : ISubjectsService
    {
        private readonly IDeletableEntityRepository<Subject> subjectsRepository;
        private readonly IDeletableEntityRepository<UserSubject> userSubjectsRepository;

        public SubjectsService(IDeletableEntityRepository<Subject> subjectsRepository, IDeletableEntityRepository<UserSubject> userSubjectsRepository)
        {
            this.subjectsRepository = subjectsRepository;
            this.userSubjectsRepository = userSubjectsRepository;
        }

        public async Task Create(CreateSubjectInputModel input)
        {
            foreach (var subject in input.Subjects)
            {
                UserSubject userSubject = new UserSubject
                {
                    UserId = input.UserId,
                    SubjectId = subject,
                };

                await this.userSubjectsRepository.AddAsync(userSubject);
            }

            await this.userSubjectsRepository.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetAll()
        {
            IEnumerable<SelectListItem> subjectsAsListItems = this.subjectsRepository
                .All()
                .OrderBy(s => s.Name)
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id,
                })
                .ToList();

            return subjectsAsListItems;
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> subjects = this.userSubjectsRepository
                .All()
                .Where(us => us.UserId == id)
                .To<T>()
                .ToList();

            return subjects;
        }

        public IEnumerable<string> GetAllNamesById(string id)
        {
            IEnumerable<string> names = this.userSubjectsRepository
                .All()
                .Where(s => s.UserId == id)
                .Select(s => s.Subject.Name)
                .ToList();

            return names.Distinct().ToList();
        }
    }
}
