using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using KomunikatyRSOUWP.ViewModels;

namespace KomunikatyRSOUWP.Views
{
    public sealed partial class DetailPage : Page
    {
        public DetailPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
            viewModel = DataContext as INavViewModel;
        }

        private INavViewModel viewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            viewModel.OnNavigatedToAsync(e.Parameter);
        }
    }
}
