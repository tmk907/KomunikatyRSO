using KomunikatyRSO.Shared.Models;
using System.Collections.Generic;

namespace KomunikatyRSO.Shared.Commands.Notifications.Models
{
    public class Preferences
    {
        public Preferences()
        {
            Categories = new Dictionary<string, bool>();
            foreach(var category in CategoriesInfo.AllCategories)
            {
                Categories[category.Slug] = false;
            }
            Provinces = new Dictionary<string, bool>();
            foreach(var province in ProvincesInfo.AllProvinces)
            {
                Provinces[province.Slug] = false;
            }
        }

        public Dictionary<string, bool> Categories { get; set; }

        public Dictionary<string,bool> Provinces { get; set; }   
    }
}
