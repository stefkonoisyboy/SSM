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
    using StudentsSocialMedia.Web.ViewModels.Home;

    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly ISubjectsService subjectsService;
        private readonly IGroupsService groupsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
            IPostsService postsService,
            ISubjectsService subjectsService,
            IGroupsService groupsService,
            UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.subjectsService = subjectsService;
            this.groupsService = groupsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            CreatePostInputModel input = new CreatePostInputModel
            {
                Subjects = this.subjectsService.GetAll(),
                GroupsItems = this.groupsService.GetAllAsListItems(user.Id),
            };

            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.Subjects = this.subjectsService.GetAll();
                input.GroupsItems = this.groupsService.GetAllAsListItems(user.Id);
                return this.View(input);
            }

            input.CreatorId = user.Id;
            await this.postsService.CreateAsync(input);

            return this.Redirect("/");
        }
    }
}
