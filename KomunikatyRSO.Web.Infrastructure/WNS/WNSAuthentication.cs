using Flurl.Http;
using KomunikatyRSO.Web.Infrastructure.Services;
using KomunikatyRSO.Web.Infrastructure.Settings;
using KomunikatyRSO.Web.Infrastructure.WNS.Models;
using System;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.WNS
{
    public class WNSAuthenticationHelper
    {
        public WNSAuthenticationHelper(WnsSettings wnsSettings, WnsTokenStorage wnsTokenStorage)
        {
            sid = wnsSettings.Sid;
            secret = wnsSettings.Secret;
            this.wnsTokenStorage = wnsTokenStorage;
        }

        private readonly string sid;
        private readonly string secret;
        private readonly WnsTokenStorage wnsTokenStorage;
        private string accessToken;

        private string AuthUrl = "https://login.live.com/accesstoken.srf";

        public string GetAccessToken()
        {
            if (String.IsNullOrEmpty(accessToken))
            {
                accessToken = wnsTokenStorage.GetToken();
            }
            return accessToken;
        }

        public async Task<string> RequestAccessToken()
        {
            var response = await AuthUrl.PostUrlEncodedAsync(new
            {
                grant_type = "client_credentials",
                client_id = sid,
                client_secret = secret,
                scope = "notify.windows.com"
            }).ReceiveJson<AccessTokenResponse>();
            accessToken = response.AccessToken;
            await wnsTokenStorage.SaveToken(accessToken);
            return accessToken;
        }
    }
}
