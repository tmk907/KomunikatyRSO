using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KomunikatyRSOUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Poradniki : Page
    {
        public Poradniki()
        {
            this.InitializeComponent();
            this.Loaded += Poradniki_Loaded;
        }

        private void Poradniki_Loaded(object sender, RoutedEventArgs e)
        {
            WebView.Navigate(new Uri("ms-appx-web:///Assets/Poradniki/Poradniki1.html"));
        }
    }
}
