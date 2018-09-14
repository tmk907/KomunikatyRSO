using KomunikatyRSO.Shared.Models;

namespace KomunikatyRSOUWP.ViewModels
{
    public class MeteoViewModel : BasicViewModel
    {
        public MeteoViewModel() : base()
        {
            catInfo = CategoriesInfo.GetBySlug("meteorologiczne");
        }
    }
}
