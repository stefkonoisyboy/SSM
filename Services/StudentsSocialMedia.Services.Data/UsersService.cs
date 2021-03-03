namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Profiles;
    using StudentsSocialMedia.Web.ViewModels.SearchUsers;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<T> GetAllByName<T>(SearchInputModel input, string id)
        {
            IEnumerable<T> users = this.usersRepository
                .All()
                .OrderBy(u => u.UserName)
                .Where(u => u.UserName.Contains(input.UserName) && u.Id != id)
                .To<T>()
                .ToList();

            return users;
        }

        public T GetById<T>(string id)
        {
            T user = this.usersRepository
                .All()
                .Where(u => u.Id == id)
                .To<T>()
                .FirstOrDefault();

            return user;
        }
    }
}
