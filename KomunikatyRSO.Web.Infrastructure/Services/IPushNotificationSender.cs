using KomunikatyRSO.Web.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Services
{
    public interface IPushNotificationSender
    {
        Task SendPushNotifications(List<string> urls, NewsDto news);
    }
}
