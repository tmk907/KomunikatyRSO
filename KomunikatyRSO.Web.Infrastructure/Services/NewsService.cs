using KomunikatyRSO.Web.Infrastructure.DTO;
using KomunikatyRSO.Shared.Api;
using KomunikatyRSO.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Services
{
    public class NewsService
    {
        private readonly RSOClient client;

        public NewsService()
        {
            client = new RSOClient();
        }

        public async Task<List<NewsDto>> GetNewNewses()
        {
            DateTime lastUpdate = DateTime.Now.Subtract(TimeSpan.FromDays(7));

            var latestNews = new List<NewsDto>(); 

            foreach(var category in CategoriesInfo.AllCategories)
            {
                foreach(var province in ProvincesInfo.AllProvinces)
                {
                    var newses = await client.GetNewsesAsync(province.Slug, category.Slug);
                    foreach(var news in newses.Where(n=>n.UpdatedAt.ToDateTime() > lastUpdate))
                    {
                        latestNews.Add(
                            new NewsDto($"woj. {province.Name}", news.Title, news.Id, province.Slug, category.Slug)
                        );
                    }
                }
            }

            return latestNews;
        }
    }
}
