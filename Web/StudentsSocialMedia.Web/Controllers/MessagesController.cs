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
    using StudentsSocialMedia.Web.ViewModels.Messages;

    public class MessagesController : Controller
    {
        private readonly IMessagesService messagesService;
        private readonly UserManager<ApplicationUser> userManager;

        public MessagesController(IMessagesService messagesService, UserManager<ApplicationUser> userManager)
        {
            this.messagesService = messagesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult AllById(string id)
        {
            AllMessagesByIdListViewModel viewModel = new AllMessagesByIdListViewModel
            {
                Messages = this.messagesService.GetAllById<AllMessagesByIdViewModel>(id),
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
        public async Task<IActionResult> Create(string id, CreateMessageInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            input.UserId = user.Id;
            input.GroupId = id;
            await this.messagesService.Create(input);

            return this.RedirectToAction(nameof(this.AllById), new { id });
        }
    }
}
