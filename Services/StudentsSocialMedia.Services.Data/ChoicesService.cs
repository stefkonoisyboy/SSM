namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Tests;

    public class ChoicesService : IChoicesService
    {
        private readonly IDeletableEntityRepository<Choice> choicesRepository;

        public ChoicesService(IDeletableEntityRepository<Choice> choicesRepository)
        {
            this.choicesRepository = choicesRepository;
        }

        public async Task<string> Create(CreateChoiceInputModel input)
        {
            Choice choice = new Choice
            {
                Text = input.Text,
            };

            await this.choicesRepository.AddAsync(choice);
            await this.choicesRepository.SaveChangesAsync();

            return choice.Id;
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> choices = this.choicesRepository
                .All()
                .Where(c => c.QuestionId == id)
                .To<T>()
                .ToList();

            return choices;
        }
    }
}
