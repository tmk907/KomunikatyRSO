using KomunikatyRSO.Shared.Models;

namespace KomunikatyRSOUWP.ViewModels
{
    public class OgolneViewModel : BasicViewModel
    {
        public OgolneViewModel() : base()
        {
            catInfo = CategoriesInfo.GetBySlug("ogolne");
        }
    }
}
