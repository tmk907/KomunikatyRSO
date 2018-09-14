using KomunikatyRSO.Shared.Api;
using KomunikatyRSO.Shared.Api.Models;
using KomunikatyRSO.UWP.Shared.Settings;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace KomunikatyRSOUWP.ViewModels
{
    public class Location
    {
        public Geopoint Geopoint { get; set; }

        public Point Anchor { get { return new Point(0.5, 1); } }
    }

    public class DetailPageViewModel : INotifyPropertyChanged, INavViewModel
    {
        public DetailPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                //i = "Designtime value";
            }
        }

        private bool error = false;
        public bool Error
        {
            get { return error; }
            set
            {
                error = value;
                NotifyPropertyChanged(nameof(Error));
            }
        }

        private bool loading = false;
        public bool Loading
        {
            get { return loading; }
            set
            {
                loading = value;
                NotifyPropertyChanged(nameof(Loading));
            }
        }

        private RSONews news = new RSONews();
        public RSONews News
        {
            get { return news; }
            set
            {
                news = value;
                NotifyPropertyChanged(nameof(News));
            }
        }

        private bool isMap = false;
        public bool IsMap { get { return isMap; } set { isMap = value; NotifyPropertyChanged(nameof(IsMap)); } }

        private string id = "0";
        public string Id { get { return id; } set { id = value; NotifyPropertyChanged(nameof(Id)); } }

        private async Task LoadNewsAsync(int id)
        {
            var newses = SettingsService.Instance.NewNewses;
            var newNews =  newses.Where(n => n.Id.Equals(id)).FirstOrDefault();
            if (newNews == null)
            {
                RSOClient rsoClient = new RSOClient();
                var t = await rsoClient.GetNewsAsync(id);
                if (t == null)
                {
                    Error = true;
                    News = new RSONews();
                }
                else
                {
                    News = t;
                }
            }
            else
            {
                News = newNews;
            }
        }

        public async Task OnNavigatedToAsync(object parameter)
        { 
            Error = false;
            Loading = true;
            IsMap = false;
            if (typeof(RSONews) == parameter.GetType())
            {
                News = (RSONews)parameter;
            }
            else
            {
                int newsId = (int)parameter;
                await LoadNewsAsync(newsId);
            }
            PrepareNews();
            //Id = (suspensionState.ContainsKey(nameof(Id))) ? suspensionState[nameof(Id)]?.ToString() : News.Id.ToString();
            Loading = false;
        }

        //public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        //{
        //    if (suspending)
        //    {
        //        suspensionState[nameof(Id)] = Id;
        //    }
        //    await Task.CompletedTask;
        //}

        private string title = "";
        public string Title { get { return title; } set { title = value; NotifyPropertyChanged(nameof(Title)); } }
        private string updatedAt = "";
        public string UpdatedAt { get { return updatedAt; } set { updatedAt = value; NotifyPropertyChanged(nameof(UpdatedAt)); } }
        private string content = "";
        public string Content { get { return content; } set { content = value; NotifyPropertyChanged(nameof(Content)); } }

        private ObservableCollection<Location> points = new ObservableCollection<Location>()
        {
            new Location()
            {
                Geopoint = new Geopoint(new BasicGeoposition() { Latitude = 52.07, Longitude = 19.48 })
            }
        };

        public ObservableCollection<Location> Points
        {
            get { return points; }
            set
            {
                points = value;
                NotifyPropertyChanged(nameof(Points));
            }
        }

        private Geopoint center = new Geopoint(new BasicGeoposition() { Latitude = 52.07, Longitude = 19.48 });
        public Geopoint Center { get { return center; } set { center = value; NotifyPropertyChanged(nameof(Center)); } }
        private Geopoint point = new Geopoint(new BasicGeoposition() { Latitude = 52.07, Longitude = 19.48 });
        public Geopoint Point { get { return point; } set { point = value; NotifyPropertyChanged(nameof(Point)); } }

        private void CompleteNews(ref RSONews news)
        {
            string trend = (news.WaterLevelTrend == "-1") ? "↓ malejący" : (news.WaterLevelTrend == "1") ? "↑ rosnący" : "↔ bez zmian";
            news.Title = "Rzeka: " + news.RiverName + ", wodowskaz: " + news.LocationName;
            news.Content = "stan wody: " + news.WaterLevelValue + "cm" + Environment.NewLine +
                "stan ostrzegawczy: " + news.WaterLevelWarningStatusValue + "cm" + Environment.NewLine +
                "stan alarmowy: " + news.WaterLevelAlarmStatusValue + "cm" + Environment.NewLine +
                "trend: " + trend;
        }

        private void PrepareNews()
        {
            if (news.RiverName != null && news.RiverName != "")
            {
                CompleteNews(ref news);
                IsMap = true;
                try
                {
                    double lat = Convert.ToDouble(news.Latitude.Substring(0, 6), System.Globalization.CultureInfo.InvariantCulture);
                    double lon = Convert.ToDouble(news.Longitude.Substring(0, 6), System.Globalization.CultureInfo.InvariantCulture);
                    BasicGeoposition position = new BasicGeoposition() { Latitude = lon, Longitude = lat };
                    Center = new Geopoint(position);
                    Points = new ObservableCollection<Location>() { new Location() { Geopoint = new Geopoint(position) } };
                }
                catch (Exception ex)
                {
                    IsMap = false;
                }
            }
            Title = news.Title;
            Content = news.Content;
            UpdatedAt = news.UpdatedAt;
        }

        public void Copy()
        {
            DataPackage dataPackage = new DataPackage();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Title);
            sb.AppendLine(UpdatedAt);
            sb.Append(Content);
            dataPackage.SetText(sb.ToString());
            Clipboard.SetContent(dataPackage);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
