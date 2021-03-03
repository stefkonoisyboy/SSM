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
    using StudentsSocialMedia.Web.ViewModels.Groups;
    using StudentsSocialMedia.Web.ViewModels.Home;
    using StudentsSocialMedia.Web.ViewModels.Posts;
    using StudentsSocialMedia.Web.ViewModels.Profiles;

    public class GroupsController : Controller
    {
        private readonly ISubjectsService subjectsService;
        private readonly IGroupsService groupsService;
        private readonly IUsersService usersService;
        private readonly IPostsService postsService;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<ApplicationUser> userManager;

        public GroupsController(
            ISubjectsService subjectsService,
            IGroupsService groupsService,
            IUsersService usersService,
            IPostsService postsService,
            IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager)
        {
            this.subjectsService = subjectsService;
            this.groupsService = groupsService;
            this.usersService = usersService;
            this.postsService = postsService;
            this.env = env;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel
            {
                Posts = this.postsService.GetAllWithGroup<AllPostsViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult ActivityById(string id)
        {
            ActivityByIdViewModel viewModel = this.groupsService.GetById<ActivityByIdViewModel>(id);
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult MembersById(string id)
        {
            AllMembersByIdListViewModel viewModel = new AllMembersByIdListViewModel
            {
                Members = this.groupsService.GetAllMembersById<AllMembersByIdViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllById(string id)
        {
            AllGroupsByIdListViewModel viewModel = new AllGroupsByIdListViewModel
            {
                Groups = this.groupsService.GetAllById<AllGroupsByIdViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AllBySubjects()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllSuggestedGroupsByIdListViewModel viewModel = new AllSuggestedGroupsByIdListViewModel
            {
                SuggestedGroups = this.groupsService.GetAllBySubjects<AllSuggestedGroupsByIdViewModel>(this.subjectsService.GetAllNamesById(user.Id), user.Id),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            CreateGroupInputModel input = new CreateGroupInputModel
            {
                SubjectsItems = this.subjectsService.GetAll(),
            };

            return this.View(input);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.SubjectsItems = this.subjectsService.GetAll();
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.CreatorId = user.Id;
            await this.groupsService.Create(input, this.env.WebRootPath);

            return this.Redirect("/");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Follow(string id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.groupsService.Follow(id, user.Id);

            return this.RedirectToAction(nameof(this.AllById), new { user.Id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Unfollow(string id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.groupsService.Unfollow(id, user.Id);

            return this.RedirectToAction(nameof(this.AllById), new { user.Id });
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.groupsService.Delete(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Edit(string id)
        {
            EditGroupInputModel input = this.groupsService.GetById<EditGroupInputModel>(id);
            input.SubjectsItems = this.subjectsService.GetAll();

            return this.View(input);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditGroupInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.SubjectsItems = this.subjectsService.GetAll();
                return this.View(input);
            }

            await this.groupsService.Update(id, input);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
