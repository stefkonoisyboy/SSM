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
    using StudentsSocialMedia.Web.ViewModels.Forum.Comments;

    public class CommentsController : ForumController
    {
        private readonly IForumCommentsService forumCommentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(IForumCommentsService forumCommentsService, UserManager<ApplicationUser> userManager)
        {
            this.forumCommentsService = forumCommentsService;
            this.userManager = userManager;
        }

        public IActionResult AllById(string id)
        {
            AllCommentsByIdListViewModel viewModel = new AllCommentsByIdListViewModel
            {
                Comments = this.forumCommentsService.GetAllById<AllCommentsByIdViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create(string id)
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(string id, CreateCommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.UserId = user.Id;
            input.PostId = id;
            await this.forumCommentsService.Create(input);

            return this.Redirect("/Forum/Categories/All");
        }
    }
}
