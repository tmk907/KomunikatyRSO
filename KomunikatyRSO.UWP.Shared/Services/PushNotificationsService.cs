using KomunikatyRSO.Shared.Commands.Accounts;
using KomunikatyRSO.Shared.Commands.Notifications;
using KomunikatyRSO.UWP.Shared.Settings;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;

namespace KomunikatyRSO.UWP.Shared.Services
{
    public class PushNotificationsService
    {
        public PushNotificationsService()
        {
            client = new PushNotificationsClient();
            secureStorage = new SecureStorage();
        }

        private readonly PushNotificationsClient client;
        private readonly ISecureStorage secureStorage;

        public bool IsRegistered => secureStorage.UserId != Guid.Empty;

        public async Task RegisterAsync()
        {
            var userId = Guid.NewGuid();
            string password = GeneratePassword();
            var command = new Register()
            {
                Password = password,
                UserId = userId,
            };
            try
            {
                var result = await client.RegisterAsync(command);
                if (result)
                {
                    secureStorage.UserId = userId;
                    secureStorage.Password = password;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task RequestTokenIfNeededAsync()
        {
            if (String.IsNullOrEmpty(secureStorage.Token) || secureStorage.TokenExpiration < DateTime.Now)
            {
                var command = new CreateToken()
                {
                    Password = secureStorage.Password,
                    UserId = secureStorage.UserId
                };
                try
                {
                    var jwtToken = await client.RequestTokenAsync(command);
                    secureStorage.Token = jwtToken.Token;
                    secureStorage.TokenExpiration = jwtToken.Expires;
                }
                catch (Exception ex)
                {
                }
            }
        }

        private string GeneratePassword()
        {
            return "password";
        }

        public async Task UpdateUserPreferencesIfNeededAsync()
        {
            System.Diagnostics.Debug.WriteLine("UpdateUserPreferencesIfNeededAsync()");
            bool updated = AppSettings.Instance.IsPreferencesUpdated;
            System.Diagnostics.Debug.WriteLine("UpdateUserPreferencesIfNeededAsync() " + updated);
            if (!updated)
            {
                await UpdateUserPreferencesAsync();
            }
        }

        public async Task UpdatePushChannelIfNeeded()
        {
            DateTimeOffset prevChannelExpirationTime = AppSettings.Instance.NotificationChannelExpirationTime;
            string prevChannelUri = AppSettings.Instance.NotificationChannelUri;
            if (prevChannelExpirationTime < DateTime.Now.AddDays(7))
            {
                try
                {
                    var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                    System.Diagnostics.Debug.WriteLine("UpdatePushChannelIfNeeded() new channel Uri={0} ExpirationTime={1}", channel.Uri, channel.ExpirationTime);
                    await UpdatePushChannelAsync(channel.Uri);
                    if (AppSettings.Instance.IsChannelUpdated)
                    {
                        AppSettings.Instance.NotificationChannelExpirationTime = channel.ExpirationTime;
                        AppSettings.Instance.NotificationChannelUri = channel.Uri;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Could not create a channel " + ex);
                    throw;
                }
            }
        }

        private async Task UpdatePushChannelAsync(string newPushChannel)
        {
            if (newPushChannel == null) return;

            await RequestTokenIfNeededAsync();

            var command = new UpdatePushChannel();
            command.Token = secureStorage.Token;
            command.UserId = secureStorage.UserId;
            command.PushChannel = newPushChannel;

            try
            {
                bool updated = await client.UpdatePushChannelAsync(command);
                AppSettings.Instance.IsChannelUpdated = updated;
            }
            catch (Exception)
            {
                AppSettings.Instance.IsChannelUpdated = false;
            }
        }

        public async Task UpdateUserPreferencesAsync()
        {
            await RequestTokenIfNeededAsync();

            var command = new UpdatePreferences();
            command.Token = secureStorage.Token;
            command.UserId = secureStorage.UserId;
            foreach (var province in AppSettings.Instance.SelectedProvinces.Where(p => p.IsSelected))
            {
                command.Preferences.Provinces[province.Slug] = true;
            }
            foreach (var category in AppSettings.Instance.SelectedCategories.Where(p => p.IsSelected))
            {
                command.Preferences.Categories[category.Slug] = true;
            }
            try
            {
                bool updated = await client.UpdatePreferencesAsync(command);
                AppSettings.Instance.IsPreferencesUpdated = updated;
            }
            catch (Exception)
            {
                AppSettings.Instance.IsPreferencesUpdated = false;
            }
        }
    }
}
