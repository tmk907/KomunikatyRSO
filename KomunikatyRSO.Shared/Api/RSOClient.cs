using KomunikatyRSO.Shared.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KomunikatyRSO.Shared.Api
{
    public class RSOClient
    {
        public JsonSerializerSettings JsonSettings { get; set; }

        private readonly UrlBuilder uriBuilder;
        private readonly HttpClient httpClient;

        public RSOClient()
        {
            uriBuilder = new UrlBuilder();
            httpClient = new HttpClient();
        }

        public RSOClient(HttpClient httpClient)
        {
            uriBuilder = new UrlBuilder();
            this.httpClient = httpClient;
        }

        private async Task<T> GetAsync<T>(string requestUrl)
        {
            try
            {
                var response = await httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public async Task<RSONews> GetNewsAsync(int id)
        {
            string url = uriBuilder.GetNews(id);
            return await GetAsync<RSONews>(url);
        }

        public async Task<List<RSONews>> GetNewsesAsync(int page = 0)
        {
            string url = uriBuilder.GetNewses(page);
            var res = await GetAsync<RSONewses>(url);
            if (res != null)
            {
                List<RSONews> list = new List<RSONews>();
                if (res.Newses != null)
                {
                    foreach (var item in res.Newses)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<RSONews>> GetNewsesAsync(string province, string category, int page = 0)
        {
            string url = uriBuilder.GetNewses(province, category, page);
            var res = await GetAsync<RSONewses>(url);
            if (res != null)
            {
                List<RSONews> list = new List<RSONews>();
                if (res.Newses != null)
                {
                    foreach (var item in res.Newses)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
