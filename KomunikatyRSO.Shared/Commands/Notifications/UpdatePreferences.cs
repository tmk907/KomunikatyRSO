using KomunikatyRSO.Shared.Commands.Notifications.Models;
using System;

namespace KomunikatyRSO.Shared.Commands.Notifications
{
    public class UpdatePreferences : AuthenticatedCommandBase
    {
        public Preferences Preferences { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
