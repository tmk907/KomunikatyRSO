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
            PushClient pn = new PushClient();
            await pn.UpdatePushChannelAsync();
            await pn.CheckUpdatePreferencesAsync();
        }

        public static bool UpdateAppSettings()
        {
            var version = Package.Current.Id.Version;
            var currentAppVersion = string.Format("{0}.{1}", version.Major, version.Minor);
            var prevAppVersion = SettingsService.Instance.PreviousAppVersion;

            if (prevAppVersion == "")
            {
                SettingsService.Instance.UserId = "-1";
                SettingsService.Instance.IsChannelUpdated = false;
                SettingsService.Instance.IsPreferencesUpdated = false;
                SettingsService.Instance.NotificationChannelUri = null;
                SettingsService.Instance.NotificationChannelExpirationTime = DateTimeOffset.MinValue;
                SettingsService.Instance.PreviousAppVersion = currentAppVersion;
            }

            if (prevAppVersion != currentAppVersion)
            {
                SettingsService.Instance.PreviousAppVersion = currentAppVersion;
            }

            return prevAppVersion != currentAppVersion;
        }
    }
}
