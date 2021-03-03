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
    using StudentsSocialMedia.Web.ViewModels.Likes;

    public class LikesController : Controller
    {
        private readonly ILikesService likesService;
        private readonly UserManager<ApplicationUser> userManager;

        public LikesController(ILikesService likesService, UserManager<ApplicationUser> userManager)
        {
            this.likesService = likesService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Like(string id, CreateLikeInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.CreatorId = user.Id;
            input.PostId = id;
            await this.likesService.Create(input);

            return this.Redirect("/");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Unlike(string id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.likesService.Delete(id, user.Id);
            return this.Redirect("/");
        }
    }
}
