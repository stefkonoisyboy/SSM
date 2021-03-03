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
    using StudentsSocialMedia.Web.ViewModels.Followers;
    using StudentsSocialMedia.Web.ViewModels.Hobbies;
    using StudentsSocialMedia.Web.ViewModels.Images;
    using StudentsSocialMedia.Web.ViewModels.Posts;
    using StudentsSocialMedia.Web.ViewModels.Profiles;

    public class ProfilesController : Controller
    {
        private readonly IPostsService postsService;
        private readonly IImagesService imagesService;
        private readonly IFollowersService followersService;
        private readonly IUsersService usersService;
        private readonly IProfilesService profilesService;
        private readonly ITownsService townsService;
        private readonly IHobbiesService hobbiesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfilesController(
            IPostsService postsService,
            IImagesService imagesService,
            IFollowersService followersService,
            IUsersService usersService,
            IProfilesService profilesService,
            ITownsService townsService,
            IHobbiesService hobbiesService,
            UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.imagesService = imagesService;
            this.followersService = followersService;
            this.usersService = usersService;
            this.profilesService = profilesService;
            this.townsService = townsService;
            this.hobbiesService = hobbiesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Timeline(string id)
        {
            ProfileViewModel viewModel = new ProfileViewModel
            {
                UserInfo = this.usersService.GetById<UserViewModel>(id),
                Posts = this.postsService.GetAllById<AllPostsViewModel>(id),
                LastPhotos = this.imagesService.GetAllLatestById<AllImagesViewModel>(id),
                Followers = this.followersService.GetAllById<AllFollowersViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult About(string id)
        {
            AboutViewModel viewModel = this.profilesService.GetAbout<AboutViewModel>(id);

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            EditProfileInputModel input = this.profilesService.GetAbout<EditProfileInputModel>(user.Id);
            input.Towns = this.townsService.GetAll();

            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Towns = this.townsService.GetAll();
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.profilesService.UpdateAsync(user.Id, input);

            return this.RedirectToAction(nameof(this.About), new { user.Id });
        }

        [Authorize]
        public IActionResult Friends(string id)
        {
            return this.View();
        }
    }
}
