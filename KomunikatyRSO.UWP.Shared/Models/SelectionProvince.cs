using KomunikatyRSO.Shared.Models;
using System.Runtime.Serialization;

namespace KomunikatyRSO.UWP.Shared.Models
{
    [DataContract]
    public class SelectionProvince : BindableBase
    {
        public SelectionProvince() { }

        public SelectionProvince(ProvinceInfo info)
        {
            Id = info.Id;
            Name = info.Name;
            Slug = info.Slug;
        }

        public SelectionProvince(ProvinceInfo info, bool isSelected)
        {
            Id = info.Id;
            Name = info.Name;
            Slug = info.Slug;
            IsSelected = isSelected;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Slug { get; set; }
        
        private bool isSelected = false;
        [DataMember]
        public bool IsSelected
        {
            get { return isSelected; }
            set { Set(ref isSelected, value); }
        }
    }
}
