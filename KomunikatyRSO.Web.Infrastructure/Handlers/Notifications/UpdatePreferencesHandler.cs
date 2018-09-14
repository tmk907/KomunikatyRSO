using KomunikatyRSO.Web.Infrastructure.Services;
using KomunikatyRSO.Shared.Commands;
using KomunikatyRSO.Shared.Commands.Notifications;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Handlers.Notifications
{
    public class UpdatePreferencesHandler : ICommandHandler<UpdatePreferences>
    {
        private readonly NotificationsService notificationsService;

        public UpdatePreferencesHandler(NotificationsService notificationsService)
        {
            this.notificationsService = notificationsService;
        }

        public async Task HandleAsync(UpdatePreferences command)
        {
            await notificationsService.UpdatePrefrencesAsync(command);
        }
    }
}
