namespace StudentsSocialMedia.Web.Areas.Forum.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using StudentsSocialMedia.Services.Data;
    using StudentsSocialMedia.Web.ViewModels.Forum.Categories;

    public class CategoriesController : ForumController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult All()
        {
            AllCategoriesListViewModel viewModel = new AllCategoriesListViewModel
            {
                Categories = this.categoriesService.GetAll<AllCategoriesViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
