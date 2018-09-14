using Newtonsoft.Json;

namespace KomunikatyRSO.Shared.Api.Models
{
    public class Provinces
    {
        public RSOProvince Province
        {
            get
            {
                return w1 ?? w2 ?? w3 ?? w4 ?? w5 ?? w6 ?? w7 ?? w8 ?? w9 ?? w10 ?? w11 ?? w12 ?? w13 ?? w14 ?? w15 ?? w16;
            }
        }

        [JsonProperty("1")]
        public RSOProvince w1 { get; set; }

        [JsonProperty("2")]
        public RSOProvince w2 { get; set; }

        [JsonProperty("3")]
        public RSOProvince w3 { get; set; }

        [JsonProperty("4")]
        public RSOProvince w4 { get; set; }

        [JsonProperty("5")]
        public RSOProvince w5 { get; set; }

        [JsonProperty("6")]
        public RSOProvince w6 { get; set; }

        [JsonProperty("7")]
        public RSOProvince w7 { get; set; }

        [JsonProperty("8")]
        public RSOProvince w8 { get; set; }

        [JsonProperty("9")]
        public RSOProvince w9 { get; set; }

        [JsonProperty("10")]
        public RSOProvince w10 { get; set; }

        [JsonProperty("11")]
        public RSOProvince w11 { get; set; }

        [JsonProperty("12")]
        public RSOProvince w12 { get; set; }

        [JsonProperty("13")]
        public RSOProvince w13 { get; set; }

        [JsonProperty("14")]
        public RSOProvince w14 { get; set; }

        [JsonProperty("15")]
        public RSOProvince w15 { get; set; }

        [JsonProperty("16")]
        public RSOProvince w16 { get; set; }
    }

    public class RSOProvince
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("slug_name")]
        public string SlugName { get; set; }
    }
}
