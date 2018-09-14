using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace KomunikatyRSOUWP.Helpers
{
    public class ThemeHelper
    {
        private static Color AppAccentColor = Color.FromArgb(0, 255, 232, 17);
        public static void ApplyAppTheme(bool isLightTheme)
        {
            System.Diagnostics.Debug.WriteLine("ThemeHelper ApplyAppTheme");
            if (isLightTheme)
            {
                Views.Shell.Instance.AppFrame.RequestedTheme = ElementTheme.Light;
                ApplyThemeToTitleBar(ElementTheme.Light);
                ApplyThemeToStatusBar(ElementTheme.Light);
            }
            else
            {
                Views.Shell.Instance.AppFrame.RequestedTheme = ElementTheme.Dark;
                ApplyThemeToTitleBar(ElementTheme.Dark);
                ApplyThemeToStatusBar(ElementTheme.Dark);
            }
        }

        public static void ApplyThemeToStatusBar(bool isLightTheme)
        {
            System.Diagnostics.Debug.WriteLine("ThemeHelper ApplyThemeToStatusBar");
            if (isLightTheme) ApplyThemeToStatusBar(ElementTheme.Light);
            else ApplyThemeToStatusBar(ElementTheme.Dark);
        }

        public static void ApplyThemeToStatusBar(ElementTheme theme)
        {
            System.Diagnostics.Debug.WriteLine("ThemeHelper ApplyThemeToStatusBar");
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                switch (theme)
                {
                    case ElementTheme.Dark:
                        statusBar.BackgroundColor = Colors.Black;
                        statusBar.BackgroundOpacity = 1;
                        statusBar.ForegroundColor = Colors.White;
                        break;
                    case ElementTheme.Light:
                        statusBar.BackgroundColor = Colors.White;
                        statusBar.BackgroundOpacity = 1;
                        statusBar.ForegroundColor = Colors.Black;
                        break;
                    default:
                        statusBar.BackgroundColor = Colors.Black;
                        statusBar.BackgroundOpacity = 1;
                        statusBar.ForegroundColor = Colors.White;
                        break;
                }
            }
        }

        public static void ApplyThemeToTitleBar(bool isLightTheme)
        {
            System.Diagnostics.Debug.WriteLine("ThemeHelper ApplyThemeToTitleBar");
            if (isLightTheme) ApplyThemeToTitleBar(ElementTheme.Light);
            else ApplyThemeToTitleBar(ElementTheme.Dark);
        }

        public static void ApplyThemeToTitleBar(ElementTheme theme)
        {
            System.Diagnostics.Debug.WriteLine("ThemeHelper ApplyThemeToTitleBar");
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    //switch (theme)
                    //{
                    //    case ElementTheme.Dark:
                    //        titleBar.BackgroundColor = AppAccentColor;
                    //        titleBar.ButtonBackgroundColor = AppAccentColor;
                    //        titleBar.ButtonForegroundColor = Colors.White;
                    //        titleBar.ForegroundColor = Colors.White;
                    //        break;
                    //    case ElementTheme.Light:
                    //        titleBar.BackgroundColor = AppAccentColor;
                    //        titleBar.ButtonBackgroundColor = AppAccentColor;
                    //        titleBar.ButtonForegroundColor = Colors.White;
                    //        titleBar.ForegroundColor = Colors.White;
                    //        break;
                    //    default:
                    //        titleBar.BackgroundColor = AppAccentColor;
                    //        titleBar.ButtonBackgroundColor = AppAccentColor;
                    //        titleBar.ButtonForegroundColor = Colors.White;
                    //        titleBar.ForegroundColor = Colors.White;
                    //        break;
                    //}
                }
            }
        }
    }
}
