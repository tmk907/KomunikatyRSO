using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace KomunikatyRSOUWP.Common
{
    public class BackgroundTaskHelper
    {
        public static async Task OnAppUpdate()
        {
            BackgroundExecutionManager.RemoveAccess();
            BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
        }

        public static void UnregisterBackgroundTask()
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == "KomunikatyRSOUWPBackgroundSettingsUpdater")
                {
                    task.Value.Unregister(true);
                    break;
                }
            }
        }

        public static async Task RegisterBackgroundTasksAsync()
        {
            //BGUpdater
            string name = "KomunikatyRSOUWPBackgroundSettingsUpdater";
            string entryPoint = "BGUpdater.BackgroundSettingsUpdater";
            TimeTrigger timeTrigger = new TimeTrigger(3*60, false);
            SystemTrigger st = new SystemTrigger(SystemTriggerType.InternetAvailable, false);
            SystemCondition internetCondition = new SystemCondition(SystemConditionType.InternetAvailable);

            try
            {
                await RegisterBackgroundTaskAsync(entryPoint, name, timeTrigger, internetCondition);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Background task registration error: {0}", ex);
            }
        }

        public static async Task RegisterBackgroundTaskAsync(string taskEntryPoint, string taskName, IBackgroundTrigger trigger, IBackgroundCondition condition)
        {
            var taskRegistered = false;

            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == taskName)
                {
                    //task.Value.Unregister(true);
                    taskRegistered = true;
                    break;
                }
            }

            if (!taskRegistered)
            {
                var builder = new BackgroundTaskBuilder();
                builder.Name = taskName;
                builder.TaskEntryPoint = taskEntryPoint;
                builder.SetTrigger(trigger);
                if (condition != null)
                {
                    builder.AddCondition(condition);
                }
                System.Diagnostics.Debug.WriteLine("task regiter");

                var status = await BackgroundExecutionManager.RequestAccessAsync();
                if (status == BackgroundAccessStatus.DeniedBySystemPolicy || status == BackgroundAccessStatus.DeniedByUser || status == BackgroundAccessStatus.Unspecified)
                {
                    System.Diagnostics.Debug.WriteLine("Cant't register access status {0}", status);
                }

                BackgroundTaskRegistration task = builder.Register();
            }
        }
    }
}
