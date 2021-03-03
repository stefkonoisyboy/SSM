namespace StudentsSocialMedia.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StudentsSocialMedia.Services.Data;
    using StudentsSocialMedia.Web.ViewModels.Followers;
    using StudentsSocialMedia.Web.ViewModels.SearchFollowers;

    public class SearchFollowersController : Controller
    {
        private readonly IFollowersService followersService;

        public SearchFollowersController(IFollowersService followersService)
        {
            this.followersService = followersService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult SearchResults(string id, SearchInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            input.UserId = id;
            AllFollowersListViewModel viewModel = new AllFollowersListViewModel
            {
                Followers = this.followersService.SearchAllByUsername<AllFollowersViewModel>(input),
            };

            return this.View(viewModel);
        }
    }
}
