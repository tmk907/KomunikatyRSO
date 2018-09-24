using Flurl;

namespace KomunikatyRSO.UWP.Shared.Services
{
    public class UrlBuilder
    {
        private string BaseUrl = "https://testrsoapi2.devrso.pl";

        public UrlBuilder()
        {
//#if DEBUG
//            BaseUrl = "http://localhost:50958";
//#endif
        }

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
