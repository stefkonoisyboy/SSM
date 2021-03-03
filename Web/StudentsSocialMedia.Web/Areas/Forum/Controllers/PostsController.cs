namespace StudentsSocialMedia.Web.Areas.Forum.Controllers
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
    using StudentsSocialMedia.Web.ViewModels.Forum.Posts;

    public class PostsController : ForumController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IForumPostsService forumPostsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(ICategoriesService categoriesService, IForumPostsService forumPostsService, UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.forumPostsService = forumPostsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            CreatePostInputModel input = new CreatePostInputModel
            {
                CategoryItems = this.categoriesService.GetAllAsSelectListItems(),
            };

            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoryItems = this.categoriesService.GetAllAsSelectListItems();
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.UserId = user.Id;
            await this.forumPostsService.Create(input);

            return this.Redirect("/Forum/Categories/All");
        }

        public IActionResult AllById(string id)
        {
            AllPostsByIdListViewModel viewModel = new AllPostsByIdListViewModel
            {
                Posts = this.forumPostsService.GetAllById<AllPostsByIdViewModel>(id),
            };

            return this.View(viewModel);
        }
    }
}
