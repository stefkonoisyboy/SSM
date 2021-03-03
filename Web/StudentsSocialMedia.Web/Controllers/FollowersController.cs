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
    using StudentsSocialMedia.Web.ViewModels.Profiles;

    public class FollowersController : Controller
    {
        private readonly IFollowersService followersService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public FollowersController(IFollowersService followersService, IUsersService usersService, UserManager<ApplicationUser> userManager)
        {
            this.followersService = followersService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult AllById(string id)
        {
            AllFollowersListViewModel viewModel = new AllFollowersListViewModel
            {
                UserInfo = this.usersService.GetById<UserViewModel>(id),
                Followers = this.followersService.GetAllById<AllFollowersViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Follow(string id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.followersService.Follow(user.Id, id);

            return this.RedirectToAction(nameof(this.AllById), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Unfollow(string id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.followersService.Unfollow(user.Id, id);

            return this.RedirectToAction(nameof(this.AllById), new { id });
        }
    }
}
