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
    using StudentsSocialMedia.Web.ViewModels.SearchUsers;

    public class SearchUsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public SearchUsersController(IUsersService usersService, UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Search()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Search(SearchInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            IEnumerable<SearchResultsViewModel> viewModel = this.usersService.GetAllByName<SearchResultsViewModel>(input, user.Id);

            return this.View("Results", viewModel);
        }
    }
}
