using System.ComponentModel;
using System.Linq;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI;
using KomunikatyRSOUWP.Helpers;
using Windows.UI.Xaml.Navigation;
using Windows.Foundation.Metadata;

namespace KomunikatyRSOUWP.Views
{
    public sealed partial class Shell : Page
    {
        private static Shell _instance;
        public static Shell Instance
        {
            get
            {
                return _instance;
            }
        }


        public Shell()
        {
            _instance = this;
            InitializeComponent();

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            if (titleBar != null)
            {
                titleBar.BackgroundColor = Color.FromArgb(255, 232, 17, 35);
                titleBar.ForegroundColor = Colors.White;
                titleBar.ButtonBackgroundColor = Color.FromArgb(255, 232, 17, 35);

                titleBar.ButtonInactiveBackgroundColor = Color.FromArgb(255, 232, 17, 35);

                titleBar.InactiveBackgroundColor = Color.FromArgb(255, 232, 17, 35);
                titleBar.InactiveForegroundColor = Colors.LightGray;
            }
            NavView.SelectedItem = NavView.MenuItems.FirstOrDefault();

            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 6))
            {
                NavView.IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
            }

            AppFrame.Navigated += AppFrame_Navigated;
        }

        private void AppFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode != NavigationMode.Back) return;
            var type = AppFrame.CurrentSourcePageType;
            System.Diagnostics.Debug.WriteLine("Select {0}", type.ToString());
            if (type == typeof(Ogolne))
            {
                NavView.SelectedItem = NavView.MenuItems[0];
            }
            else if (type == typeof(Meteo))
            {
                NavView.SelectedItem = NavView.MenuItems[1];
            }
            else if (type == typeof(Drogowe))
            {
                NavView.SelectedItem = NavView.MenuItems[2];
            }
            else if (type == typeof(Hydro))
            {
                NavView.SelectedItem = NavView.MenuItems[3];
            }
            else if (type == typeof(StanyWod))
            {
                NavView.SelectedItem = NavView.MenuItems[4];
            }
            else if (type == typeof(Poradniki))
            {
                NavView.SelectedItem = NavView.MenuItems[5];
            }
            else if(type == typeof(SettingsPage))
            {
                NavView.SelectedItem = NavView.SettingsItem;
            }
        }

        public Frame AppFrame
        {
            get
            {
                return ContentFrame;
            }
            set
            {
                ContentFrame = value;
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Invoked {0}", args.InvokedItem.ToString());
            if (args.IsSettingsInvoked)
            {
                App.NavigationService.NavigateTo(typeof(SettingsPage));
            }
            else
            {
                switch (args.InvokedItem as string)
                {
                    case "Ogólne":
                        App.NavigationService.NavigateTo(typeof(Ogolne), "ogolne");
                        break;
                    case "Meteorologiczne":
                        App.NavigationService.NavigateTo(typeof(Meteo), "meteorologiczne");
                        break;
                    case "Informacje drogowe":
                        App.NavigationService.NavigateTo(typeof(Drogowe), "informacje-drogowe");
                        break;
                    case "Hydrologiczne":
                        App.NavigationService.NavigateTo(typeof(Hydro), "hydrologiczne");
                        break;
                    case "Stany wód":
                        App.NavigationService.NavigateTo(typeof(StanyWod), "stany-wod");
                        break;
                    case "Poradniki":
                        App.NavigationService.NavigateTo(typeof(Poradniki));
                        break;
                }
            }
        }
    }
}
