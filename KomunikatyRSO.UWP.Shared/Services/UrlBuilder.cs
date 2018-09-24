using Flurl;

namespace KomunikatyRSO.UWP.Shared.Services
{
    public class UrlBuilder
    {
        private string BaseUrl = "http://46.101.229.216:36477";

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
