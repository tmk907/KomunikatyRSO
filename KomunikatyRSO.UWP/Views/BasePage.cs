using KomunikatyRSO.UWP.Shared.Extensions;
using KomunikatyRSO.UWP.Shared.Settings;
using KomunikatyRSOUWP.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KomunikatyRSOUWP.Views
{
    public class BasePage : Page
    {
        public BasePage()
        {
            
        }

        protected void Init()
        {
            //NavigationCacheMode = NavigationCacheMode.Required;
            viewModel = DataContext as INavViewModel;
        }

        protected INavViewModel viewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.RequestedTheme = AppSettings.Instance.AppTheme.ToElementTheme();
            viewModel.OnNavigatedToAsync(e.Parameter);
        }
    }
}
