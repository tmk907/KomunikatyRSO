using System.Collections.Generic;
using System.Linq;

namespace KomunikatyRSO.Shared.Models
{
    public class CategoryInfo
    {
        public string Slug { get; set; }
        public string Name { get; set; }
    }

    public class CategoriesInfo
    {
        static CategoriesInfo()
        {
            AllCategories = new List<CategoryInfo>()
            {
                new CategoryInfo() { Slug = "ogolne", Name = "Ogólne" },
                new CategoryInfo() { Slug = "meteorologiczne", Name = "Meteorologiczne" },
                new CategoryInfo() { Slug = "hydrologiczne", Name = "Hydrologiczne" },
                new CategoryInfo() { Slug = "informacje-drogowe", Name = "Informacje drogowe" },
                new CategoryInfo() { Slug = "stany-wod", Name = "Stany wód" },
                
            };
            All = new CategoryInfo() { Slug = "wszystkie", Name = "Wszystkie" };
        }

        public static readonly List<CategoryInfo> AllCategories;
        public static readonly CategoryInfo All;

        public const string Wszystkie = "wszystkie";
        public const string Ogolne = "ogolne";
        public const string Hydro = "hydrologiczne";
        public const string Meteo = "meteorologiczne";
        public const string Drogowe = "informacje-drogowe";
        public const string StanyWod = "stany-wod";

        public static CategoryInfo GetByName(string name)
        {
            return AllCategories.Where(p => p.Name.Equals(name)).FirstOrDefault();
        }

        public static CategoryInfo GetBySlug(string slug)
        {
            return AllCategories.Where(p => p.Slug.Equals(slug)).FirstOrDefault();
        }
    }
}
