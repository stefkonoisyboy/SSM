namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Data.Models.Enumerations;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Profiles;

    public class ProfilesService : IProfilesService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public ProfilesService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public T GetAbout<T>(string id)
        {
            T user = this.usersRepository
                .All()
                .Where(u => u.Id == id)
                .To<T>()
                .FirstOrDefault();

            return user;
        }

        public async Task UpdateAsync(string id, EditProfileInputModel input)
        {
            ApplicationUser user = this.usersRepository.All()
                .FirstOrDefault(u => u.Id == id);

            user.UserName = input.UserName;
            user.NormalizedUserName = input.UserName;
            user.Email = input.Email;
            user.NormalizedEmail = input.Email;
            user.PhoneNumber = input.PhoneNumber;
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.BirthDate = input.BirthDate;
            user.Gender = (Gender)Enum.Parse(typeof(Gender), input.Gender);
            user.TownId = input.TownId;

            await this.usersRepository.SaveChangesAsync();
        }
    }
}
