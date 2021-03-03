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
    using StudentsSocialMedia.Web.ViewModels.Hobbies;

    public class HobbiesService : IHobbiesService
    {
        private readonly IDeletableEntityRepository<Hobby> hobbiesRepository;
        private readonly IDeletableEntityRepository<UserHobby> userHobbiesRepository;

        public HobbiesService(IDeletableEntityRepository<Hobby> hobbiesRepository, IDeletableEntityRepository<UserHobby> userHobbiesRepository)
        {
            this.hobbiesRepository = hobbiesRepository;
            this.userHobbiesRepository = userHobbiesRepository;
        }

        public async Task Create(CreateHobbyInputModel input)
        {
            foreach (var hobby in input.Hobbies)
            {
                UserHobby userHobby = new UserHobby
                {
                    UserId = input.UserId,
                    HobbyId = hobby,
                };

                await this.userHobbiesRepository.AddAsync(userHobby);
            }

            await this.userHobbiesRepository.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetAll()
        {
            IEnumerable<SelectListItem> hobbiesItems = this.hobbiesRepository
                .All()
                .Select(h => new SelectListItem
                {
                    Value = h.Id,
                    Text = h.Name,
                })
                .ToList();

            return hobbiesItems;
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> hobbies = this.userHobbiesRepository
                .All()
                .Where(uh => uh.UserId == id)
                .To<T>()
                .ToList();

            return hobbies;
        }
    }
}
