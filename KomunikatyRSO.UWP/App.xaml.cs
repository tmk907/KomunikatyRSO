using Windows.UI.Xaml;
using Windows.ApplicationModel.Activation;
using System;
using KomunikatyRSOUWP.Common;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;
using KomunikatyRSOUWP.Views;
using KomunikatyRSOUWP.Services;
using KomunikatyRSOUWP.Helpers;
using KomunikatyRSO.UWP.Shared.Settings;

namespace KomunikatyRSOUWP
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            bool isLight = AppSettings.Instance.AppTheme == ApplicationTheme.Light;
            Application.Current.RequestedTheme = isLight ? ApplicationTheme.Light : ApplicationTheme.Dark;
            bool isAppUpdated = SettingsUpdater.UpdateAppSettings();
            RegisterBGTask(isAppUpdated);
        }

        public static NavigationService NavigationService { get; protected set; }

        public enum Pages
        {
            Details,
            Drogowe,
            Hydro,
            Meteo,
            Nowe,
            Ogolne,
            Poradniki,
            StanyWod,
            Ustawienia
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Shell shell = Window.Current.Content as Shell;

            if (shell == null)
            {
                // Create a AppShell to act as the navigation context and navigate to the first page
                shell = new Shell();

                shell.AppFrame.NavigationFailed += OnNavigationFailed;

                NavigationService = new NavigationService(shell.AppFrame);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Should load state from previous run
                }
                Window.Current.Content = shell;
            }

            if (NavigationService == null)
            {
                NavigationService = new NavigationService(shell.AppFrame);
            }

            if (e.Kind == ActivationKind.Launch)
            {
                SettingsUpdater.SendSettingsToServerAsync();
            }
            NavigationService.NavigateTo(typeof(Ogolne));

            // Ensure the current window is active
            Window.Current.Activate();

            ApplyThemes();
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;
        }


        protected override void OnActivated(IActivatedEventArgs e)
        {
            Shell shell = Window.Current.Content as Shell;

            if (shell == null)
            {
                // Create a AppShell to act as the navigation context and navigate to the first page
                shell = new Shell();

                shell.AppFrame.NavigationFailed += OnNavigationFailed;

                NavigationService = new NavigationService(shell.AppFrame);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Should load state from previous run
                }
                Window.Current.Content = shell;
            }

            if (NavigationService == null)
            {
                NavigationService = new NavigationService(shell.AppFrame);
            }

            // Handle toast activation
            if (e is ToastNotificationActivatedEventArgs)
            {
                var toastargs = e as ToastNotificationActivatedEventArgs;
                int id;
                if (Int32.TryParse(toastargs.Argument, out id))
                {
                    NavigationService.NavigateTo(typeof(DetailPage), id);
                }
                else
                {
                    NavigationService.NavigateTo(typeof(Ogolne));
                }
            }

            // TODO: Handle other types of activation

            // Ensure the current window is active
            Window.Current.Activate();

            ApplyThemes();
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private void ApplyThemes()
        {
            bool isLight = AppSettings.Instance.AppTheme == ApplicationTheme.Light;
            ThemeHelper.ApplyAppTheme(isLight);
        }

        private async void RegisterBGTask(bool isUpdated)
        {
            if (isUpdated)
            {
                await BackgroundTaskHelper.OnAppUpdate();
            }
            await BackgroundTaskHelper.RegisterBackgroundTasksAsync();
        }
    }
}
