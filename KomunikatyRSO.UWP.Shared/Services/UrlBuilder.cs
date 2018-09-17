using Flurl;

namespace KomunikatyRSO.UWP.Shared.Services
{
    public class UrlBuilder
    {
        public readonly string BaseUrl = "https://testrsoapi2.devrso.pl";

        public string Register()
        {
            return BaseUrl.AppendPathSegment("account");
        }

        public string RequestToken()
        {
            return BaseUrl.AppendPathSegment("account").AppendPathSegment("token");
        }

        public string UpdatePushChannel()
        {
            return BaseUrl.AppendPathSegment("notifications");
        }

        public string UpdatePreferences()
        {
            return BaseUrl.AppendPathSegment("notifications").AppendPathSegment("preferences");
        }
    }
}
