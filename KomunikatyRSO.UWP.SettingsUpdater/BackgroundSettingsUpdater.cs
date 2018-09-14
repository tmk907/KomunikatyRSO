using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace KomunikatyRSO.UWP.SettingsUpdater
{
    public sealed class BackgroundSettingsUpdater : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral = null;
        private bool completed = false;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);

            System.Diagnostics.Debug.WriteLine("BackgroundSettingsUpdater started");
            await UpdateSettingsAsync();
            System.Diagnostics.Debug.WriteLine("BackgroundSettingsUpdater finished");

            _deferral.Complete();
        }

        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            _deferral.Complete();
        }

        private async Task UpdateSettingsAsync()
        {
            Shared.Settings.SettingsUpdater.UpdateAppSettings();
            await Shared.Settings.SettingsUpdater.SendSettingsToServerAsync();
            completed = true;
        }
    }
}
