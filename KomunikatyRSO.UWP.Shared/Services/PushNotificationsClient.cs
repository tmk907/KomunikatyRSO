using Flurl.Http;
using KomunikatyRSO.Shared.Commands.Accounts;
using KomunikatyRSO.Shared.Commands.Notifications;
using KomunikatyRSO.Shared.Dto;
using System.Threading.Tasks;

namespace KomunikatyRSO.UWP.Shared.Services
{
    public class PushNotificationsClient
    {
        private UrlBuilder urlBuilder;

        private string authToken;

        public PushNotificationsClient()
        {
            urlBuilder = new UrlBuilder();
            authToken = "";
        }

        public void SetAuthToken(string token)
        {
            authToken = token;
        }

        public async Task<bool> RegisterAsync(Register command)
        {
            var url = urlBuilder.Register();
            var response = await url.PostJsonAsync(command);
            return response.IsSuccessStatusCode;
        }

        public async Task<JwtDto> RequestTokenAsync(CreateToken command)
        {
            var url = urlBuilder.RequestToken();
            var response = await url.PostJsonAsync(command).ReceiveJson<JwtDto>();
            return response;
        }

        public async Task<bool> UpdatePreferencesAsync(UpdatePreferences command)
        {
            var url = urlBuilder.UpdatePreferences();
            var response = await url.WithOAuthBearerToken(authToken).PutJsonAsync(command);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePushChannelAsync(UpdatePushChannel command)
        {
            var url = urlBuilder.UpdatePushChannel();
            var response = await url.WithOAuthBearerToken(authToken).PutJsonAsync(command);
            return response.IsSuccessStatusCode;
        }
    }
}
