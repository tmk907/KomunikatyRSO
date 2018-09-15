using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using KomunikatyRSO.UWP.Shared.Settings;
using KomunikatyRSO.Shared.Api;
using KomunikatyRSO.Shared.Models;
using KomunikatyRSO.Shared.Api.Models;

namespace KomunikatyRSOUWP.ViewModels
{
    public class BasicViewModel : INotifyPropertyChanged, INavViewModel
    {
        public BasicViewModel()
        {
            rsoClient = new RSOClient();
            provInfo = ProvincesInfo.All;
            catInfo = CategoriesInfo.All;
            header = catInfo.Name;
            SelectedProvinces = new List<string>();
            foreach(var item in ProvincesInfo.AllProvinces)
            {
                SelectedProvinces.Add(item.Name);
            }
            SelectedProvinces.Add("Wszystkie");
            SelectedProvinces.Add("Z ustawień");
            if (AppSettings.Instance.SelectedProvinces.Where(p=>p.IsSelected).Count() == 0)
            {
                SelectedProvince = SelectedProvinces.Find(s => s.Equals("Wszystkie"));
            }
            else
            {
                SelectedProvince = SelectedProvinces.Find(s => s.Equals("Z ustawień"));
            }
        }

        protected RSOClient rsoClient;
        protected CategoryInfo catInfo;
        protected ProvinceInfo provInfo;

        public List<string> SelectedProvinces;

        protected string selectedProvince;
        public string SelectedProvince
        {
            get { return selectedProvince; }
            set
            {
                if (value != selectedProvince)
                {
                    FilterNewses(value);
                }
                selectedProvince = value;
                //NotifyPropertyChanged(nameof(SelectedProvince));
            }
        }

        protected void FilterNewses(string value)
        {
            if (value == "Wszystkie")
            {
                Newses = allNewses;
            }
            else if (value == "Z ustawień")
            {
                var ust = AppSettings.Instance.SelectedProvinces.Where(p => p.IsSelected).ToList();
                Newses = allNewses.Where(n => ust.Exists(p => p.Slug.Equals(n.Provinces.Province.SlugName))).ToList();
            }
            else
            {
                Newses = allNewses.Where(n => n.Provinces.Province.SlugName.Equals(ProvincesInfo.GetByName(value).Slug)).ToList();
            }
            if (Newses.Count == 0)
            {
                ErrorText = "Nie ma nowych komunikatów dla wybranych województw";
                Error = true;
            }
            else
            {
                Error = false;
            }
        }

        protected List<RSONews> allNewses = new List<RSONews>();

        protected List<RSONews> newses = new List<RSONews>();
        public List<RSONews> Newses
        {
            get { return newses; }
            set
            {
                newses = value;
                NotifyPropertyChanged(nameof(Newses));
            }
        }

        protected string header;
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                NotifyPropertyChanged(nameof(Header));
            }
        }

        protected bool error = false;
        public bool Error
        {
            get { return error; }
            set
            {
                error = value;
                NotifyPropertyChanged(nameof(Error));
            }
        }

        protected string errorText;
        public string ErrorText
        {
            get { return errorText; }
            set
            {
                errorText = value;
                NotifyPropertyChanged(nameof(ErrorText));
            }
        }

        protected bool loading = false;
        public bool Loading
        {
            get { return loading; }
            set
            {
                loading = value;
                NotifyPropertyChanged(nameof(Loading));
            }
        }

        protected async Task LoadNewsesAsync(int page = 0)
        {
            var t = await rsoClient.GetNewsesAsync(provInfo.Slug, catInfo.Slug, page);
            if (t == null)
            {
                Error = true;
                ErrorText = "Nie można pobrać danych";
            }
            else if (t.Count == 0)
            {
                Error = true;
                ErrorText = "Nie ma nowych komunikatów";
            }
            else
            {
                allNewses = ParseNewses(t);
                FilterNewses(selectedProvince);
            }
        }

        public void ItemClicked(object sender, ItemClickEventArgs e)
        {
            App.NavigationService.NavigateTo(typeof(Views.DetailPage), ((RSONews)e.ClickedItem));
        }

        public async Task OnNavigatedToAsync(object parameter)
        {
            Error = false;
            Loading = true;
            var cat = parameter as string;
            if (cat == null) cat = "ogolne";
            if (cat != catInfo.Slug || allNewses.Count == 0)
            {
                catInfo = CategoriesInfo.GetBySlug(cat);
                Header = catInfo.Name;
                await LoadNewsesAsync();
            }
            else if (allNewses.Count > 0)
            {
                FilterNewses(selectedProvince);
            }
            Loading = false;
        }

        protected virtual List<RSONews> ParseNewses(List<RSONews> list)
        {
            return list;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
