using KomunikatyRSO.Shared.Models;
using System.Runtime.Serialization;

namespace KomunikatyRSO.UWP.Shared.Models
{
    [DataContract]
    public class SelectionCategory : BindableBase
    {
            public SelectionCategory() { }

            public SelectionCategory(CategoryInfo info)
            {
                Name = info.Name;
                Slug = info.Slug;
            }

            public SelectionCategory(CategoryInfo info, bool isSelected)
            {
                Name = info.Name;
                Slug = info.Slug;
                IsSelected = isSelected;
            }

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
