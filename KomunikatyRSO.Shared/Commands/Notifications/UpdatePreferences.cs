using KomunikatyRSO.Shared.Commands.Notifications.Models;
using System;

namespace KomunikatyRSO.Shared.Commands.Notifications
{
    public class UpdatePreferences : AuthenticatedCommandBase
    {
        public UpdatePreferences()
        {
            Preferences = new Preferences();
            ModificationDate = DateTime.Now;
        }

        public Preferences Preferences { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
