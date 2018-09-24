using KomunikatyRSO.Web.Infrastructure.DTO;
using System.Collections.Generic;

namespace KomunikatyRSO.Web.Infrastructure.Models
{
    public class GroupedNews
    {
        public GroupedNews(string categorySlug, string provinceSlug)
        {
            CategorySlug = categorySlug;
            ProvinceSlug = provinceSlug;
            Newses = new List<NewsDto>();
        }

        public string CategorySlug { get; }
        public string ProvinceSlug { get; }

        public List<NewsDto> Newses { get; }
    }
}
