namespace KomunikatyRSO.Shared.Api
{
    public class UrlBuilder
    {
        public const string BaseNewsDetailUrl = "http://komunikaty.tvp.pl/komunikaty";
        public const string BaseAllNewsesUrl = "http://komunikaty.tvp.pl/komunikatyxml";

        public string GetNewses(int page = 0)
        {
            return $"{BaseAllNewsesUrl}/wszystkie/wszystkie/{page}?_format=json";
        }

        public string GetNewses(string province, string category, int page = 0)
        {
            return $"{BaseAllNewsesUrl}/{province}/{category}/{page}?_format=json";
        }

        public string GetNews(int id)
        {
            return $"{BaseNewsDetailUrl}/{id}/detale?_format=json";
        }
    }
}
