namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<ForumCategory> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<ForumCategory> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IEnumerable<T> categories = this.categoriesRepository
                .All()
                .OrderBy(c => c.Title)
                .To<T>()
                .ToList();

            return categories;
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            IEnumerable<SelectListItem> items = this.categoriesRepository
                .All()
                .Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id,
                })
                .ToList();

            return items;
        }
    }
}
