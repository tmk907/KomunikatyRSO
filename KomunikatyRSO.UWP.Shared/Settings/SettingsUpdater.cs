using KomunikatyRSO.UWP.Shared.Services;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace KomunikatyRSO.UWP.Shared.Settings
{
    public class SettingsUpdater
    {
        public static async Task SendSettingsToServerAsync()
        {
            var pns = new PushNotificationsService();

            if (!pns.IsRegistered)
            {
                await pns.RegisterAsync();
                await pns.RequestTokenIfNeededAsync();
            }

            await pns.UpdatePushChannelIfNeeded();
            await pns.UpdateUserPreferencesIfNeededAsync();
        }

        public static bool UpdateAppSettings()
        {
            var version = Package.Current.Id.Version;
            var currentAppVersion = string.Format("{0}.{1}", version.Major, version.Minor);
            var prevAppVersion = AppSettings.Instance.PreviousAppVersion;

            if (prevAppVersion == "")
            {
                AppSettings.Instance.IsChannelUpdated = false;
                AppSettings.Instance.IsPreferencesUpdated = false;
                AppSettings.Instance.NotificationChannelUri = null;
                AppSettings.Instance.NotificationChannelExpirationTime = DateTimeOffset.MinValue;
                AppSettings.Instance.PreviousAppVersion = currentAppVersion;
            }

            if (prevAppVersion != currentAppVersion)
            {
                AppSettings.Instance.PreviousAppVersion = currentAppVersion;
            }

            return prevAppVersion != currentAppVersion;
        }
    }
}
