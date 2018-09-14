using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace KomunikatyRSOUWP.Converters
{
    public class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool a = (bool)value;
            if (a) return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Visibility a = (Visibility)value;
            if (Visibility.Collapsed.Equals(a)) return false;
            else return true;
        }
    }
}
