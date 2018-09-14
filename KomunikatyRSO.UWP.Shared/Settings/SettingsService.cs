using System;
using System.Collections.Generic;
using KomunikatyRSO.Shared.Api.Models;
using KomunikatyRSO.UWP.Shared.Extensions;
using KomunikatyRSO.UWP.Shared.Models;
using Template10.Services.SettingsService;
using Windows.UI.Xaml;

namespace KomunikatyRSO.UWP.Shared.Settings
{
    public class SettingsService
    {
        public static SettingsService Instance { get; } = new SettingsService();
        ISettingsHelper _helper;
        private SettingsService()
        {
            _helper = new SettingsHelper();
        }

        public ApplicationTheme AppTheme
        {
            get
            {
                var theme = ApplicationTheme.Dark;
                var value = _helper.Read<string>(nameof(AppTheme), theme.ToString());
                return Enum.TryParse<ApplicationTheme>(value, out theme) ? theme : ApplicationTheme.Dark;
            }
            set
            {
                _helper.Write(nameof(AppTheme), value.ToString());
                (Window.Current.Content as FrameworkElement).RequestedTheme = value.ToElementTheme();
                //Views.Shell.HamburgerMenu.RefreshStyles(value, true);
            }
        }

        public List<SelectionProvince> SelectedProvinces
        {
            get { return _helper.Read<List<SelectionProvince>>(nameof(SelectedProvinces), new List<SelectionProvince>()); }
            set { _helper.Write(nameof(SelectedProvinces), value); }
        }

        public List<SelectionCategory> SelectedCategories
        {
            get { return _helper.Read<List<SelectionCategory>>(nameof(SelectedCategories), new List<SelectionCategory>()); }
            set { _helper.Write(nameof(SelectedCategories), value); }
        }

        public DateTime LastUpdate
        {
            get { return _helper.Read<DateTime>(nameof(LastUpdate), DateTime.Now); }
            set { _helper.Write(nameof(LastUpdate), value); }
        }

        public List<RSONews> NewNewses
        {
            get { return _helper.Read<List<RSONews>>(nameof(NewNewses), new List<RSONews>()); }
            set { _helper.Write(nameof(NewNewses), value); }
        }

        public string NotificationChannelUri
        {
            get { return _helper.Read<string>(nameof(NotificationChannelUri), null); }
            set { _helper.Write(nameof(NotificationChannelUri), value); }
        }

        public DateTimeOffset NotificationChannelExpirationTime
        {
            get { return _helper.Read<DateTimeOffset>(nameof(NotificationChannelExpirationTime), DateTimeOffset.MinValue); }
            set { _helper.Write(nameof(NotificationChannelExpirationTime), value); }
        }

        public bool IsChannelUpdated
        {
            get { return _helper.Read<bool>(nameof(IsChannelUpdated), false); }
            set { _helper.Write(nameof(IsChannelUpdated), value); }
        }

        public bool IsPreferencesUpdated
        {
            get { return _helper.Read<bool>(nameof(IsPreferencesUpdated), false); }
            set { _helper.Write(nameof(IsPreferencesUpdated), value); }
        }

        public string UserId
        {
            get { return _helper.Read<string>(nameof(UserId), "-1"); }
            set { _helper.Write(nameof(UserId), value); }
        }

        public string PreviousAppVersion
        {
            get { return _helper.Read<string>(nameof(PreviousAppVersion), ""); }
            set { _helper.Write(nameof(PreviousAppVersion), value); }
        }
    }
}
