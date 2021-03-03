namespace StudentsSocialMedia.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Data;
    using StudentsSocialMedia.Web.ViewModels.Images;
    using StudentsSocialMedia.Web.ViewModels.Profiles;

    public class ImagesController : Controller
    {
        private readonly IImagesService imagesService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment env;

        public ImagesController(IImagesService imagesService, IUsersService usersService, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            this.imagesService = imagesService;
            this.usersService = usersService;
            this.userManager = userManager;
            this.env = env;
        }

        [Authorize]
        public IActionResult AllById(string id)
        {
            AllImagesByIdListViewModel viewModel = new AllImagesByIdListViewModel
            {
                UserInfo = this.usersService.GetById<UserViewModel>(id),
                Images = this.imagesService.GetAllById<AllImagesByIdViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateImageInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.UserId = user.Id;
            await this.imagesService.Create(input, this.env.WebRootPath);

            return this.RedirectToAction(nameof(this.AllById), new { user.Id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.imagesService.Delete(id);
            return this.RedirectToAction(nameof(this.AllById), new { user.Id });
        }

        [Authorize]
        public IActionResult ChangeProfilePicture()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeProfilePicture(ChangeProfilePictureInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.UserId = user.Id;
            await this.imagesService.ChangeProfilePicture(input, this.env.WebRootPath);

            return this.RedirectToAction(nameof(this.AllById), new { user.Id });
        }
    }
}
