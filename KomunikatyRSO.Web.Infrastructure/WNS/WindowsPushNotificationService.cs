using Flurl.Http;
using KomunikatyRSO.Web.Infrastructure.DTO;
using KomunikatyRSO.Web.Infrastructure.Services;
using KomunikatyRSO.Web.Infrastructure.Settings;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.WNS
{
    public class WindowsPushNotificationService : IPushNotificationSender
    {
        private readonly WnsSettings wnsSettings;
        private readonly WNSAuthenticationHelper wnsAuthentication;

        public WindowsPushNotificationService(WnsSettings wnsSettings, WNSAuthenticationHelper wnsAuthentication)
        {
            this.wnsSettings = wnsSettings;
            this.wnsAuthentication = wnsAuthentication;
        }

        public async Task SendPushNotifications(List<string> urls, NewsDto news)
        {
            string toast = BuildToast(news);
            var accessToken = wnsAuthentication.GetAccessToken();
            foreach (var url in urls)
            {
                try
                {
                    var response = await url.WithOAuthBearerToken(accessToken)
                            .WithHeader("Content-Type", "text/xml")
                            .WithHeader("Content-Length", toast.Length)
                            .WithHeader("X-WNS-Type", "wns/toast")
                            .SendAsync(HttpMethod.Post);
                }
                catch (FlurlHttpException ex)
                {
                    if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                    {
                        accessToken = await wnsAuthentication.RequestAccessToken();

                        var response = await url.WithOAuthBearerToken(accessToken)
                            .WithHeader("Content-Type", "text/xml")
                            .WithHeader("Content-Length", toast.Length)
                            .WithHeader("X-WNS-Type", "wns/toast")
                            .SendAsync(HttpMethod.Post);
                    }
                    else if (ex.Call.HttpStatus == System.Net.HttpStatusCode.NotAcceptable)
                    {
                        await Task.Delay(500);

                        var response = await url.WithOAuthBearerToken(accessToken)
                            .WithHeader("Content-Type", "text/xml")
                            .WithHeader("Content-Length", toast.Length)
                            .WithHeader("X-WNS-Type", "wns/toast")
                            .SendAsync(HttpMethod.Post);
                    }
                }
            }
        }

        private string BuildToast(NewsDto news)
        {
            return BuildToast(news.NewsId.ToString(), news.Title, news.Body);
        }

        private string BuildToast(string id, string header, string body)
        {
            return 
                $"<?xml version=\"1.0\"?>"+
                    $"<toast launch=\"{id}\">"+
                        "<visual>"+
                            "<binding template=\"ToastGeneric\">"+
                                "<text>{ header}</text>"+
                                "< text hint-wrap=\"true\">{body}</text>"+
                            "<//binding>"+
                        "<//visual>"+
                    "<//toast>";

        }
    }


}
