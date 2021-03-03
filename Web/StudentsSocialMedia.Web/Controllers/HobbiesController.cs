namespace StudentsSocialMedia.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Data;
    using StudentsSocialMedia.Web.ViewModels.Hobbies;

    public class HobbiesController : Controller
    {
        private readonly IHobbiesService hobbiesService;
        private readonly UserManager<ApplicationUser> userManager;

        public HobbiesController(IHobbiesService hobbiesService, UserManager<ApplicationUser> userManager)
        {
            this.hobbiesService = hobbiesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult AllById(string id)
        {
            AllHobbiesByIdListViewModel viewModel = new AllHobbiesByIdListViewModel
            {
                Hobbies = this.hobbiesService.GetAllById<AllHobbiesByIdViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            CreateHobbyInputModel input = new CreateHobbyInputModel
            {
                HobbiesItems = this.hobbiesService.GetAll(),
            };

            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateHobbyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.HobbiesItems = this.hobbiesService.GetAll();
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.UserId = user.Id;
            await this.hobbiesService.Create(input);

            return this.Redirect($"/Profiles/About/{user.Id}");
        }
    }
}
