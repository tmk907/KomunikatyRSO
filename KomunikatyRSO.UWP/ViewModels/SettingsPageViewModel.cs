using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel;
using System.Text;
using System.ComponentModel;
using KomunikatyRSOUWP.Helpers;
using KomunikatyRSO.UWP.Shared.Services;
using KomunikatyRSO.UWP.Shared.Models;
using KomunikatyRSO.UWP.Shared.Settings;
using KomunikatyRSO.Shared.Models;

namespace KomunikatyRSOUWP.ViewModels
{
    public class AppStatus
    {
        public string Version { get; set; }
        public string DateUpdated { get; set; }
        public string SelectedProvinces { get; set; }
        public string SelectedCategories { get; set; }
        public string UserId { get; set; }
        public string Other { get; set; }

        public AppStatus()
        {
            var version = Package.Current.Id.Version;
            Version = string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            DateUpdated = Package.Current.InstalledDate.ToString("d-MM-yyyy");
            StringBuilder sb = new StringBuilder();
            foreach(var cat in AppSettings.Instance.SelectedCategories.Where(c=>c.IsSelected))
            {
                sb.Append(cat.Name).Append(", ");
            }
            if (AppSettings.Instance.SelectedCategories.Where(c => c.IsSelected).Count() > 0) sb.Remove(sb.Length - 2, 2);
            SelectedCategories = sb.ToString();
            sb.Clear();
            foreach(var prov in AppSettings.Instance.SelectedProvinces.Where(p=>p.IsSelected))
            {
                sb.Append(prov.Name).Append(", ");
            }
            if (AppSettings.Instance.SelectedProvinces.Where(p => p.IsSelected).Count() > 0) sb.Remove(sb.Length - 2, 2);
            SelectedProvinces = sb.ToString();
            UserId = AppSettings.Instance.UserId;
            Other = AppSettings.Instance.IsChannelUpdated ? "" : "c:false ";
            Other += AppSettings.Instance.IsPreferencesUpdated ? "" : "p:false ";
            Other += string.Format("{0}.{1}", version.Major, version.Minor) == AppSettings.Instance.PreviousAppVersion ? "" : AppSettings.Instance.PreviousAppVersion;
        }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Wersja: ").Append(Version).AppendLine();
            sb.Append("Data aktualizacji: ").Append(DateUpdated).AppendLine();
            sb.Append("Id: ").Append(UserId).AppendLine();
            sb.Append("Wybrane kategorie: ").Append(SelectedCategories).AppendLine();
            sb.Append("Wybrane województwa: ").Append(SelectedProvinces).AppendLine();
            sb.Append(Other).AppendLine();
            return sb.ToString();
        }
    }

    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        private PushNotificationsService pushService = new PushNotificationsService();

        public SettingsPageViewModel()
        {
            SelectedProvinces = new ObservableCollection<SelectionProvince>();
            SelectedCategories = new ObservableCollection<SelectionCategory>();

            if (DesignMode.DesignModeEnabled)
            {
                foreach (var p in ProvincesInfo.AllProvinces)
                {
                    SelectedProvinces.Add(new SelectionProvince(p));
                }
            }
            else
            {
                SelectedProvinces.Add(new SelectionProvince(ProvincesInfo.All));
                _settings = AppSettings.Instance;
                var list = _settings.SelectedProvinces;
                if (list.Count == 0)
                {
                    foreach (var p in ProvincesInfo.AllProvinces)
                    {
                        SelectedProvinces.Add(new SelectionProvince(p));
                    }
                }
                else
                {
                    bool allSelected = true;

                    foreach (var item in list)
                    {
                        SelectedProvinces.Add(item);
                        allSelected = allSelected && item.IsSelected;
                    }

                    SelectedProvinces.FirstOrDefault().IsSelected = allSelected;
                }
                var list2 = _settings.SelectedCategories;
                if (list2.Count == 0)
                {
                    foreach(var item in CategoriesInfo.AllCategories.Where(c => c.Slug != CategoriesInfo.StanyWod))
                    {
                        SelectedCategories.Add(new SelectionCategory(item, false));
                    }
                }
                else
                {
                    foreach(var item in list2.Where(c => c.Slug != CategoriesInfo.StanyWod))
                    {
                        SelectedCategories.Add(item);
                    }
                }
            }
        }
    
        AppSettings _settings;

        public bool UseLightThemeButton
        {
            get { return _settings.AppTheme.Equals(ApplicationTheme.Light); }
            set
            {
                _settings.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark;
                ThemeHelper.ApplyAppTheme(value);
                NotifyPropertyChanged(nameof(UseLightThemeButton));
            }
        }

        public ObservableCollection<SelectionProvince> SelectedProvinces;

        public ObservableCollection<SelectionCategory> SelectedCategories;

        public async void ProvinceSelectionChanged(object sender, ItemClickEventArgs e)
        {
            await ChangeProvinceSelection((SelectionProvince)e.ClickedItem);
        }

        public async void CategorySelectionChanged(object sender, ItemClickEventArgs e)
        {
            await ChangeCategorySelection((SelectionCategory)e.ClickedItem);
        }

        public async Task ChangeProvinceSelection(SelectionProvince sp)
        {
            sp.IsSelected = !sp.IsSelected;
            if (sp.Slug == ProvincesInfo.All.Slug)
            {
                foreach (var item in SelectedProvinces)
                {
                    if (item.Slug != ProvincesInfo.All.Slug) item.IsSelected = sp.IsSelected;
                }
            }
            _settings.SelectedProvinces = SelectedProvinces.Skip(1).ToList();
            await pushService.UpdateUserPreferencesAsync();
        }

        public async Task ChangeCategorySelection(SelectionCategory cat)
        {
            cat.IsSelected = !cat.IsSelected;
            _settings.SelectedCategories = SelectedCategories.ToList();
            await pushService.UpdateUserPreferencesAsync();
        }

        public Uri Logo => new Uri("ms-appx:///Assets/Icons/AppLogo.png");//Windows.ApplicationModel.Package.Current.Logo;

        public string DisplayName => Package.Current.DisplayName;

        public string Publisher => Package.Current.PublisherDisplayName;

        public string Version
        {
            get
            {
                var v = Package.Current.Id.Version;
                return $"{v.Major}.{v.Minor}.{v.Build}";//.{v.Revision}";
            }
        }

        public async void RateApp()
        {
            //HockeyClient.Current.TrackEvent("Rate app button");
            //ApplicationSettingsHelper.SaveSettingsValue(AppConstants.IsReviewed, -1);
            var uri = new Uri("ms-windows-store://review/?ProductId=9nblggh6jdhg");
            await Launcher.LaunchUriAsync(uri);
        }

        private AppStatus appStatus;
        public AppStatus AppStatus
        {
            get
            {
                if (appStatus == null)
                {
                    appStatus = new AppStatus();
                }
                return appStatus;
            }
            set
            {
                appStatus = value;
                NotifyPropertyChanged(nameof(AppStatus));
            }
        }

        public void UpdateAppStatus()
        {
            AppStatus = new AppStatus();
        }

        public async void SendEmail()
        {
            var emailMessage = new Windows.ApplicationModel.Email.EmailMessage();
            emailMessage.Subject = Package.Current.DisplayName;
            UpdateAppStatus();
            emailMessage.Body = AppStatus.ToString();
            emailMessage.To.Add(new Windows.ApplicationModel.Email.EmailRecipient("komunikaty-rso@outlook.com"));
            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }

        private ElementTheme ToElementTheme(ApplicationTheme theme)
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
