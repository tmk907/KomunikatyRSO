namespace KomunikatyRSO.Shared.Commands.Notifications
{
    public class UpdatePushChannel : AuthenticatedCommandBase
    {
        public string PushChannel { get; set; }
    }
}
