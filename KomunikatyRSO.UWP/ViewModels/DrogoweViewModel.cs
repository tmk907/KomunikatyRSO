using KomunikatyRSO.Shared.Models;

namespace KomunikatyRSOUWP.ViewModels
{
    public class DrogoweViewModel : BasicViewModel
    {
        public DrogoweViewModel() : base()
        {
            catInfo = CategoriesInfo.GetBySlug("informacje-drogowe");
        }
    }
}
