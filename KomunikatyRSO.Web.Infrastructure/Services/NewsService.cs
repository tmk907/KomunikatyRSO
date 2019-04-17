using KomunikatyRSO.Web.Infrastructure.DTO;
using KomunikatyRSO.Shared.Api;
using KomunikatyRSO.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KomunikatyRSO.Web.Infrastructure.Models;

namespace KomunikatyRSO.Web.Infrastructure.Services
{
    public class NewsService
    {
        private readonly RSOClient client;

        public NewsService()
        {
            client = new RSOClient();
        }

        public async Task<List<GroupedNews>> GetLatestNews(DateTime lastUpdateUtc)
        {
            var latestNews = new List<GroupedNews>();

            for (int i = 0; i < CategoriesInfo.AllCategories.Count; i++)
            {
                CategoryInfo category = CategoriesInfo.AllCategories[i];
                foreach (var province in ProvincesInfo.AllProvinces)
                {
                    var groupedNews = new GroupedNews(category.Slug, province.Slug);
                    var newses = await client.GetNewsesAsync(province.Slug, category.Slug);
                    foreach(var news in newses.Where(n => IsNewer(n.UpdatedAt.ToDateTime(), lastUpdateUtc)))
                    {
                        groupedNews.Newses.Add
                        (
                            new NewsDto($"woj. {province.Name}", news.Title, news.Id, province.Slug, category.Slug)
                        );
                    }
                    latestNews.Add(groupedNews);
                }
            }

            return latestNews;
        }

        private readonly TimeSpan server = TimeSpan.FromHours(2);

        private bool IsNewer(DateTime updatedAt, DateTime lastUpdateUtc)
        {
            return updatedAt > (lastUpdateUtc + server);
        }
    }
}
