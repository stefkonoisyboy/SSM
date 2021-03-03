namespace StudentsSocialMedia.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StudentsSocialMedia.Services.Data;
    using StudentsSocialMedia.Web.ViewModels.Groups;
    using StudentsSocialMedia.Web.ViewModels.SearchGroups;

    public class SearchGroupsController : Controller
    {
        private readonly ISubjectsService subjectsService;
        private readonly IGroupsService groupsService;

        public SearchGroupsController(ISubjectsService subjectsService, IGroupsService groupsService)
        {
            this.subjectsService = subjectsService;
            this.groupsService = groupsService;
        }

        [Authorize]
        public IActionResult Search()
        {
            SearchInputModel input = new SearchInputModel
            {
                SubjectsItems = this.subjectsService.GetAll(),
            };
            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Search(SearchInputModel input)
        {
            AllSuggestedGroupsByIdListViewModel viewModel = new AllSuggestedGroupsByIdListViewModel
            {
                SuggestedGroups = this.groupsService.GetAllByNameAndSubjects<AllSuggestedGroupsByIdViewModel>(input),
            };

            return this.View("SearchResults", viewModel);
        }
    }
}
