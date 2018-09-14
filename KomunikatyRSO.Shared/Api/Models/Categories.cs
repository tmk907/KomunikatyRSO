using Newtonsoft.Json;
using System.Collections.Generic;

namespace KomunikatyRSO.Shared.Api.Models
{
    public class Category
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }

    public class Categories
    {

        [JsonProperty("categories")]
        public IList<Category> CategoriesList { get; set; }
    }
}
