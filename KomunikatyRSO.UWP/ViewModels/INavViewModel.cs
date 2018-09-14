using System.Threading.Tasks;

namespace KomunikatyRSOUWP.ViewModels
{
    public interface INavViewModel
    {
        Task OnNavigatedToAsync(object parameter);
    }
}
