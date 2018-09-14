using System;

namespace KomunikatyRSOUWP.Services
{
    public interface INavigationService
    {
        void NavigateBack();
        void NavigateTo(Type pageType, object parameter = null);
    }
}