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
    using StudentsSocialMedia.Web.ViewModels.Subjects;

    public class SubjectsController : Controller
    {
        private readonly ISubjectsService subjectsService;
        private readonly UserManager<ApplicationUser> userManager;

        public SubjectsController(ISubjectsService subjectsService, UserManager<ApplicationUser> userManager)
        {
            this.subjectsService = subjectsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            CreateSubjectInputModel input = new CreateSubjectInputModel
            {
                SubjectsItems = this.subjectsService.GetAll(),
            };

            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubjectInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.SubjectsItems = this.subjectsService.GetAll();
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.UserId = user.Id;
            await this.subjectsService.Create(input);

            return this.Redirect($"/Profiles/About/{user.Id}");
        }
    }
}
