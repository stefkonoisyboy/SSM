namespace StudentsSocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Data.Common.Repositories;
    using StudentsSocialMedia.Data.Models;
    using StudentsSocialMedia.Services.Mapping;
    using StudentsSocialMedia.Web.ViewModels.Images;

    public class ImagesService : IImagesService
    {
        private readonly IDeletableEntityRepository<Image> imagesRepository;

        public ImagesService(IDeletableEntityRepository<Image> imagesRepository)
        {
            this.imagesRepository = imagesRepository;
        }

        public async Task ChangeProfilePicture(ChangeProfilePictureInputModel input, string path)
        {
            Image image = new Image
            {
                Id = "profile" + Guid.NewGuid().ToString(),
                Extension = Path.GetExtension(input.Image.FileName),
                UserId = input.UserId,
            };

            await this.imagesRepository.AddAsync(image);

            string physicalPath = $"{path}/images/users/{image.Id}{image.Extension}";
            using Stream stream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(stream);

            await this.imagesRepository.SaveChangesAsync();
        }

        public async Task Create(CreateImageInputModel input, string path)
        {
            foreach (var image in input.Images)
            {
                string extension = Path.GetExtension(image.FileName);

                Image dbImage = new Image
                {
                    Extension = extension,
                    UserId = input.UserId,
                };

                await this.imagesRepository.AddAsync(dbImage);

                string physicalPath = $"{path}/images/users/{dbImage.Id}{extension}";
                using Stream stream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(stream);
            }

            await this.imagesRepository.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            Image image = this.imagesRepository.All().FirstOrDefault(i => i.Id == id);
            this.imagesRepository.Delete(image);

            await this.imagesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllById<T>(string id)
        {
            IEnumerable<T> images = this.imagesRepository
                .All()
                .OrderByDescending(i => i.CreatedOn)
                .Where(i => i.UserId == id)
                .To<T>()
                .ToList();

            return images;
        }

        public IEnumerable<T> GetAllLatestById<T>(string id)
        {
            IEnumerable<T> images = this.imagesRepository
               .All()
               .OrderByDescending(i => i.CreatedOn)
               .Take(9)
               .Where(i => i.UserId == id)
               .To<T>()
               .ToList();

            return images;
        }
    }
}
