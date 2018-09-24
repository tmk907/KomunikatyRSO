using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Services
{
    public class TimedHostedService : BackgroundService
    {
        public TimedHostedService(IServiceProvider services)
        {
            this.services = services;
        }

        private IServiceProvider services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("MyServiceA is starting.");

            stoppingToken.Register(() => Console.WriteLine("MyServiceA is stopping."));

            TimeSpan delay = TimeSpan.FromMinutes(10);
            DateTime lastUpdate = DateTime.Now;

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("MyServiceA is doing background work.");

                using (var scope = services.CreateScope())
                {
                    var newsService = scope.ServiceProvider.GetRequiredService<NewsService>();
                    var lastestNews = await newsService.GetLatestNews(lastUpdate);
                    lastUpdate = DateTime.Now;

                    var notificationsService = scope.ServiceProvider.GetRequiredService<NotificationsService>();
                    var pushNotificationSender = scope.ServiceProvider.GetRequiredService<IPushNotificationSender>();
                    foreach (var group in lastestNews)
                    {
                        var urls = await notificationsService.GetSubsciberUrlsAsync(group.CategorySlug, group.ProvinceSlug);
                        foreach(var news in group.Newses)
                        {
                            await pushNotificationSender.SendPushNotifications(urls, news);
                        }
                    }
                }

                await Task.Delay(delay, stoppingToken);
            }

            Console.WriteLine("MyServiceA background task is stopping.");
        }
    }
}
