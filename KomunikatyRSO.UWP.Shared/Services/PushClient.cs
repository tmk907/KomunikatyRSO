using KomunikatyRSO.UWP.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;

namespace KomunikatyRSO.UWP.Shared.Services
{
    public class PushClient
    {
        private readonly string newChannelUrl;
        private readonly string updatedChannelUrl;
        private readonly string updatePreferencesUrl;
        private readonly string checkChannelUrl;

        public PushClient()
        {
#if DEBUG
            newChannelUrl = "https://komunikaty-rso.000webhostapp.com/rso-push-test/newChannel.php";
            updatedChannelUrl = "https://komunikaty-rso.000webhostapp.com/rso-push-test/updateChannel.php";
            updatePreferencesUrl = "https://komunikaty-rso.000webhostapp.com/rso-push-test/updatePreferences.php";
            checkChannelUrl = "https://komunikaty-rso.000webhostapp.com/rso-push-test/checkChannel.php";
#else
            newChannelUrl = "https://komunikaty-rso.000webhostapp.com/rso-push/newChannel.php";
            updatedChannelUrl = "https://komunikaty-rso.000webhostapp.com/rso-push/updateChannel.php";
            updatePreferencesUrl = "https://komunikaty-rso.000webhostapp.com/rso-push/updatePreferences.php";
            checkChannelUrl = "https://komunikaty-rso.000webhostapp.com/rso-push/checkChannel.php";
#endif
        }

        public async Task UpdatePushChannelAsync()
        {
            System.Diagnostics.Debug.WriteLine("UpdatePushChannel");
            int i = 0;
            bool updated = false;
            while (i < 3 && !updated)
            {
                try
                {
                    await RequestPNChannelAsync();
                    updated = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("UpdatePushChannel Exception " + ex);
                    updated = false;
                    await Task.Delay(10000);
                }
                i++;
            }
            System.Diagnostics.Debug.WriteLine("UpdatePushChannel finished");
        }

        private async Task RequestPNChannelAsync()
        {
            System.Diagnostics.Debug.WriteLine("RequestPNChannel");
            DateTimeOffset prevChannelExpirationTime = AppSettings.Instance.NotificationChannelExpirationTime;
            string prevChannelUri = AppSettings.Instance.NotificationChannelUri;
            var userId = AppSettings.Instance.UserId;
            //if (userId == "-1") return;
            bool isChannelOK = await IsChannelOK(userId);

            if (isChannelOK)
            {
                if (prevChannelExpirationTime > DateTime.Now.AddDays(7))
                {
                    System.Diagnostics.Debug.WriteLine("RequestPNChannel channel not expired");

                    if (!AppSettings.Instance.IsChannelUpdated)
                    {
                        System.Diagnostics.Debug.WriteLine("RequestPNChannel update channel");
                        await SendChannelToServerAsync(prevChannelUri);
                        return;
                    }
                    return;
                }
            }

            PushNotificationChannel channel = null;

            try
            {
                channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                System.Diagnostics.Debug.WriteLine("RequestPNChannel new channel Uri={0} ExpirationTime={1}", channel.Uri, channel.ExpirationTime);
                if (prevChannelUri != channel.Uri)
                {
                    await SendChannelToServerAsync(channel.Uri);
                    AppSettings.Instance.NotificationChannelExpirationTime = channel.ExpirationTime;
                    AppSettings.Instance.NotificationChannelUri = channel.Uri;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("RequestPNChannel Could not create a channel " + ex);
                // Could not create a channel. 
                throw;
            }
        }

        private async Task SendChannelToServerAsync(string channelUri)
        {
            System.Diagnostics.Debug.WriteLine("SendChannelToServer");
            if (channelUri == null) return;
            string response = "";
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("ChannelUri", channelUri);

            string userId = AppSettings.Instance.UserId;
            if (userId == "-1")
            {
                response = await SendNewChannelAsync(data);

                if (response.Contains("ChannelUpdated"))
                {
                    AppSettings.Instance.IsChannelUpdated = true;
                    string newUserId = response.Split('!')[1];
                    AppSettings.Instance.UserId = newUserId;
                }
                else
                {
                    AppSettings.Instance.IsChannelUpdated = false;
                }
            }
            else
            {
                data.Add("UserId", userId.ToString());
                response = await SendUpdatedChannelAsync(data);

                if (response.Contains("ChannelUpdated"))
                {
                    AppSettings.Instance.IsChannelUpdated = true;
                }
                else
                {
                    AppSettings.Instance.IsChannelUpdated = false;
                }
            }
        }

        public async Task CheckUpdatePreferencesAsync()
        {
            System.Diagnostics.Debug.WriteLine("CheckUpdatePreferences");
            bool updated = AppSettings.Instance.IsPreferencesUpdated;
            System.Diagnostics.Debug.WriteLine("CheckUpdatePreferences " + updated);
            if (!updated)
            {
                await UpdateUserPreferencesAsync();
            }
        }

        public async Task UpdateUserPreferencesAsync()
        {
            System.Diagnostics.Debug.WriteLine("UpdateUserPreferences");
            var userId = AppSettings.Instance.UserId;
            if (userId == "-1") return;

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("UserId", userId.ToString());
            foreach (var province in AppSettings.Instance.SelectedProvinces.Where(p => p.IsSelected))
            {
                data.Add(province.Slug, "1");
            }
            foreach (var category in AppSettings.Instance.SelectedCategories.Where(p => p.IsSelected))
            {
                data.Add(category.Slug, "1");
            }
            string dateMiliseconds = ((long)DateTime.Now.ToUniversalTime()
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds).ToString();
            data.Add("DateMiliseconds", dateMiliseconds);

            string response = await SendUserPreferencesAsync(data);

            if (response.Contains("PreferencesUpdated"))
            {
                System.Diagnostics.Debug.WriteLine("PreferencesUpdated true");
                AppSettings.Instance.IsPreferencesUpdated = true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("PreferencesUpdated false");
                AppSettings.Instance.IsPreferencesUpdated = false;
            }
        }

        private async Task<string> SendNewChannelAsync(Dictionary<string, string> data)
        {
            System.Diagnostics.Debug.WriteLine("SendNewChannel");
            string response = await PostDataAsync(data, newChannelUrl);
            return response;
        }

        private async Task<bool> IsChannelOK(string id)
        {
            var userId = AppSettings.Instance.UserId;
            if (userId == "-1") return false;
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("UserId", id);
            string response = await SendCheckChannelAsync(data);

            if (response == "OK")
            {
                System.Diagnostics.Debug.WriteLine("Channel OK");
                return true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Channel not OK");
                return false;
            }
        }

        private async Task<string> SendUpdatedChannelAsync(Dictionary<string, string> data)
        {
            System.Diagnostics.Debug.WriteLine("SendUpdatedChannel");
            string response = await PostDataAsync(data, updatedChannelUrl);
            return response;
        }

        private async Task<string> SendUserPreferencesAsync(Dictionary<string, string> data)
        {
            System.Diagnostics.Debug.WriteLine("SendUserPreferences");
            string response = await PostDataAsync(data, updatePreferencesUrl);
            return response;
        }

        private async Task<string> SendCheckChannelAsync(Dictionary<string, string> data)
        {
            System.Diagnostics.Debug.WriteLine("SendCheckChannel");
            string response = await PostDataAsync(data, checkChannelUrl);
            return response;
        }

        private async Task<string> PostDataAsync(Dictionary<string, string> data, string url)
        {
            string response = "";
            using (HttpClient httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(data))
                {

                    try
                    {
                        using (var result = await httpClient.PostAsync(url, content))
                        {
                            response = await result.Content.ReadAsStringAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("PostData " + ex);
                    }
                }
            }
            return response;
        }
    }
}
