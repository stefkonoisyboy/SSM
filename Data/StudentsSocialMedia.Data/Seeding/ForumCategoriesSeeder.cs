namespace StudentsSocialMedia.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSocialMedia.Data.Models;

    public class ForumCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ForumCategories.Any())
            {
                return;
            }

            ICollection<ForumCategory> categories = new List<ForumCategory>()
            {
                new ForumCategory
                {
                    Title = "Bulgarian",
                    RemoteImageUrl = "https://lh3.ggpht.com/WLE97onepen0pL1ksI-MLEv0Z6Xjk-Vb9901zY2Z8AhFlSiOO4zH7IsWXP2knV0unA=w170",
                },
                new ForumCategory
                {
                    Title = "Mathematics",
                    RemoteImageUrl = "https://tse1.mm.bing.net/th?id=OIP.xiyYuWuKocZnxP-PCBa8wgAAAA&pid=Api&P=0&w=198&h=170",
                },
                new ForumCategory
                {
                    Title = "English",
                    RemoteImageUrl = "https://tse2.mm.bing.net/th?id=OIP.huIdPjXjGVVVxVXzu-jaqQHaE7&pid=Api&P=0&w=238&h=160",
                },
                new ForumCategory
                {
                    Title = "Biology",
                    RemoteImageUrl = "https://tse4.mm.bing.net/th?id=OIP.UQywq6GGld8bOUEte4YePwHaFj&pid=Api&P=0&w=263&h=198",
                },
                new ForumCategory
                {
                    Title = "Chemistry",
                    RemoteImageUrl = "https://tse2.mm.bing.net/th?id=OIP.F0RAk6gTsGnlG9QKqXUDDwHaEK&pid=Api&P=0&w=282&h=160",
                },
                new ForumCategory
                {
                    Title = "Physics",
                    RemoteImageUrl = "https://tse1.mm.bing.net/th?id=OIP.89bsm-OC1-KwW6ECRRaAKQHaEK&pid=Api&P=0&w=275&h=155",
                },
                new ForumCategory
                {
                    Title = "Philosophy",
                    RemoteImageUrl = "https://tse3.explicit.bing.net/th?id=OIP.K2CHR42u-rFByOlgEnwV2QHaD3&pid=Api&P=0&w=305&h=160",
                },
                new ForumCategory
                {
                    Title = "P.E.",
                    RemoteImageUrl = "https://tse2.mm.bing.net/th?id=OIP.jVl1YfEL5anAAXDJjtTkRwHaEx&pid=Api&P=0&w=237&h=153",
                },
                new ForumCategory
                {
                    Title = "I.T.",
                    RemoteImageUrl = "https://tse4.mm.bing.net/th?id=OIP.TNxA5KjVB5cmx-O5DcUdhwHaEK&pid=Api&P=0&w=286&h=162",
                },
                new ForumCategory
                {
                    Title = "History",
                    RemoteImageUrl = "https://tse1.mm.bing.net/th?id=OIP.qbbwF7hJzeQEqkkyDrLIbgHaFC&pid=Api&P=0&w=246&h=168",
                },
                new ForumCategory
                {
                    Title = "Geography",
                    RemoteImageUrl = "https://tse1.mm.bing.net/th?id=OIP.Gosmv8-XGXUj4_o5FkoAvQHaEH&pid=Api&P=0&w=275&h=154",
                },
                new ForumCategory
                {
                    Title = "Art",
                    RemoteImageUrl = "https://tse4.mm.bing.net/th?id=OIP.zpSCLtvuI_vCGCwkBNse3gHaE7&pid=Api&P=0&w=232&h=156",
                },
                new ForumCategory
                {
                    Title = "Music",
                    RemoteImageUrl = "https://lh3.ggpht.com/WLE97onepen0pL1ksI-MLEv0Z6Xjk-Vb9901zY2Z8AhFlSiOO4zH7IsWXP2knV0unA=w170",
                },
                new ForumCategory
                {
                    Title = "Other",
                    RemoteImageUrl = "https://tse2.mm.bing.net/th?id=OIP.jlLfubEysL9v36AmY18LUAHaFj&pid=Api&P=0&w=201&h=151",
                },
                new ForumCategory
                {
                    Title = "Suggestions",
                    RemoteImageUrl = "https://tse1.mm.bing.net/th?id=OIP.Pxx_Y4JBhuc1JDWNeIcB4AAAAA&pid=Api&P=0&w=183&h=160",
                },
                new ForumCategory
                {
                    Title = "Problems with the app",
                    RemoteImageUrl = "https://tse2.mm.bing.net/th?id=OIP.CITW9wOQskakLpugyrgcUQHaEo&pid=Api&P=0&w=245&h=154",
                },
            };

            await dbContext.ForumCategories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }
    }
}
