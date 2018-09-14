using KomunikatyRSO.Web.Infrastructure.Services;
using KomunikatyRSO.Shared.Commands;
using KomunikatyRSO.Shared.Commands.Notifications;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Handlers.Notifications
{
    public class UpdatePushChannelHandler : ICommandHandler<UpdatePushChannel>
    {
        private readonly NotificationsService notificationsService;

        public UpdatePushChannelHandler(NotificationsService notificationsService)
        {
            this.notificationsService = notificationsService;
        }

        public async Task HandleAsync(UpdatePushChannel command)
        {
            await notificationsService.UpdatePushChannelAsync(command.UserId, command.PushChannel);
        }
    }
}
