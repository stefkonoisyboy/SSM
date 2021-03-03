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
    using StudentsSocialMedia.Web.ViewModels.Tests;

    public class QuestionsController : Controller
    {
        private readonly IQuestionsService questionsService;
        private readonly ITestsService testsService;
        private readonly UserManager<ApplicationUser> userManager;

        public QuestionsController(IQuestionsService questionsService, ITestsService testsService, UserManager<ApplicationUser> userManager)
        {
            this.questionsService = questionsService;
            this.testsService = testsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            CreateQuestionInputModel input = new CreateQuestionInputModel
            {
                TestsItems = this.testsService.GetAllAsListItemsById(user.Id),
            };

            return this.View(input);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionInputModel input)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                input.TestsItems = this.testsService.GetAllAsListItemsById(user.Id);
                return this.View(input);
            }

            await this.questionsService.Create(input);
            this.TempData["Message"] = "Question is created successfully!";

            return this.Redirect("/");
        }
    }
}
