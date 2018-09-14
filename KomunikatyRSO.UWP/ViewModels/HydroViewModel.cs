using KomunikatyRSO.Shared.Models;

namespace KomunikatyRSOUWP.ViewModels
{
    public class HydroViewModel : BasicViewModel
    {
        public HydroViewModel() : base()
        {
            catInfo = CategoriesInfo.GetBySlug("hydrologiczne");
        }
    }
}
