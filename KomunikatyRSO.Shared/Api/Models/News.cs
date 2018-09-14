using Newtonsoft.Json;
using System.Collections.Generic;

namespace KomunikatyRSO.Shared.Api.Models
{
    public class RSONewses
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("newses")]
        public IList<RSONews> Newses { get; set; }
    }

    public class Pagination
    {

        [JsonProperty("totalitems")]
        public int Totalitems { get; set; }

        [JsonProperty("itemsperpage")]
        public int Itemsperpage { get; set; }
    }

    public class RSONews
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("shortcut")]
        public string Shortcut { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("rso_alarm")]
        public int? RsoAlarm { get; set; }

        [JsonProperty("rso_icon")]
        public object RsoIcon { get; set; }

        [JsonProperty("valid_from")]
        public string ValidFrom { get; set; }

        [JsonProperty("valid_to")]
        public string ValidTo { get; set; }

        [JsonProperty("repetition")]
        public object Repetition { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("water_level_value")]
        public string WaterLevelValue { get; set; }

        [JsonProperty("water_level_warning_status_value")]
        public string WaterLevelWarningStatusValue { get; set; }

        [JsonProperty("water_level_alarm_status_value")]
        public string WaterLevelAlarmStatusValue { get; set; }

        [JsonProperty("water_level_trend")]
        public string WaterLevelTrend { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("river_name")]
        public string RiverName { get; set; }

        [JsonProperty("location_name")]
        public string LocationName { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("provinces")]
        public Provinces Provinces { get; set; }
    }
}
