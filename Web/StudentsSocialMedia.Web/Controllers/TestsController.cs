namespace StudentsSocialMedia.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Data;
    using StudentsSocialMedia.Web.ViewModels.Questions;
    using StudentsSocialMedia.Web.ViewModels.Tests;

    public class TestsController : Controller
    {
        private readonly ISubjectsService subjectsService;
        private readonly ITestsService testsService;
        private readonly IQuestionsService questionsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TestsController(
            ISubjectsService subjectsService,
            ITestsService testsService,
            IQuestionsService questionsService,
            UserManager<ApplicationUser> userManager)
        {
            this.subjectsService = subjectsService;
            this.testsService = testsService;
            this.questionsService = questionsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> AllBySubjects()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            AllTestsBySubjectsListViewModel viewModel = new AllTestsBySubjectsListViewModel
            {
                Tests = this.testsService.GetAllBySubjects<AllTestsBySubjectsViewModel>(this.subjectsService.GetAllNamesById(user.Id), user.Id),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            CreateTestInputModel input = new CreateTestInputModel
            {
                SubjectsItems = this.subjectsService.GetAll(),
            };

            return this.View(input);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.SubjectsItems = this.subjectsService.GetAll();
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.CreatorId = user.Id;
            await this.testsService.Create(input);
            this.TempData["Message"] = "Test is created successfully!";

            return this.Redirect("/");
        }

        [Authorize(Roles = "Student")]
        public IActionResult DoTest(string id)
        {
            AllQuestionsByIdListViewModel viewModel = new AllQuestionsByIdListViewModel
            {
                Questions = this.questionsService.GetAllById<AllQuestionsByIdViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DoTest(string id, IFormCollection formCollection)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            await this.testsService.Submit(id, user.Id);
            int points = await this.testsService.GetPoints(formCollection, user.Id);
            await this.testsService.IncreaseUserPoints(user.Id, points);
            string userId = user.Id;
            this.ViewBag.Points = points;
            this.ViewBag.TestId = id;

            return this.View("Result");
        }

        [Authorize]
        public IActionResult Review(string id)
        {
            AllQuestionsByIdListViewModel viewModel = new AllQuestionsByIdListViewModel
            {
                Questions = this.questionsService.GetAllById<AllQuestionsByIdViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Ranking()
        {
            IEnumerable<RankingViewModel> ranking = this.testsService.GetUsersInRanking<RankingViewModel>();

            return this.View(ranking);
        }
    }
}
