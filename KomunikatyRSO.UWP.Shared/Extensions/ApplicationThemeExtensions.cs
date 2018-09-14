using Windows.UI.Xaml;

namespace KomunikatyRSO.UWP.Shared.Extensions
{
    public static class ApplicationThemeExtensions
    {
        public static ElementTheme ToElementTheme(this ApplicationTheme theme)
        {
            switch (theme)
            {
                case ApplicationTheme.Light:
                    return ElementTheme.Light;
                case ApplicationTheme.Dark:
                default:
                    return ElementTheme.Dark;
            }
        }
    }
}
