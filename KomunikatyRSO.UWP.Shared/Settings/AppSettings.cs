using System;
using System.Collections.Generic;
using KomunikatyRSO.Shared.Api.Models;
using KomunikatyRSO.UWP.Shared.Extensions;
using KomunikatyRSO.UWP.Shared.Models;
using Windows.UI.Xaml;

namespace KomunikatyRSO.UWP.Shared.Settings
{
    public class AppSettings
    {
        public static AppSettings Instance { get; } = new AppSettings();

        SettingsHelper helper;

        private AppSettings()
        {
            helper = new SettingsHelper();
        }

        public ApplicationTheme AppTheme
        {
            get
            {
                var theme = ApplicationTheme.Dark;
                var value = helper.Read<string>(nameof(AppTheme), theme.ToString());
                return Enum.TryParse<ApplicationTheme>(value, out theme) ? theme : ApplicationTheme.Dark;
            }
            set
            {
                helper.Save(nameof(AppTheme), value.ToString());
                (Window.Current.Content as FrameworkElement).RequestedTheme = value.ToElementTheme();
                //Views.Shell.HamburgerMenu.RefreshStyles(value, true);
            }
        }

        public List<SelectionProvince> SelectedProvinces
        {
            get { return helper.Read<List<SelectionProvince>>(nameof(SelectedProvinces), new List<SelectionProvince>()); }
            set { helper.Save(nameof(SelectedProvinces), value); }
        }

        public List<SelectionCategory> SelectedCategories
        {
            get { return helper.Read<List<SelectionCategory>>(nameof(SelectedCategories), new List<SelectionCategory>()); }
            set { helper.Save(nameof(SelectedCategories), value); }
        }

        public DateTime LastUpdate
        {
            get { return helper.Read<DateTime>(nameof(LastUpdate), DateTime.Now); }
            set { helper.Save(nameof(LastUpdate), value); }
        }

        public List<RSONews> NewNewses
        {
            get { return helper.Read<List<RSONews>>(nameof(NewNewses), new List<RSONews>()); }
            set { helper.Save(nameof(NewNewses), value); }
        }

        public string NotificationChannelUri
        {
            get { return helper.Read<string>(nameof(NotificationChannelUri), null); }
            set { helper.Save(nameof(NotificationChannelUri), value); }
        }

        public DateTimeOffset NotificationChannelExpirationTime
        {
            get { return helper.Read<DateTimeOffset>(nameof(NotificationChannelExpirationTime), DateTimeOffset.MinValue); }
            set { helper.Save(nameof(NotificationChannelExpirationTime), value); }
        }

        public bool IsChannelUpdated
        {
            get { return helper.Read<bool>(nameof(IsChannelUpdated), false); }
            set { helper.Save(nameof(IsChannelUpdated), value); }
        }

        public bool IsPreferencesUpdated
        {
            get { return helper.Read<bool>(nameof(IsPreferencesUpdated), false); }
            set { helper.Save(nameof(IsPreferencesUpdated), value); }
        }

        public string UserId
        {
            get { return helper.Read<string>(nameof(UserId), "-1"); }
            set { helper.Save(nameof(UserId), value); }
        }

        public string PreviousAppVersion
        {
            get { return helper.Read<string>(nameof(PreviousAppVersion), ""); }
            set { helper.Save(nameof(PreviousAppVersion), value); }
        }
    }
}
