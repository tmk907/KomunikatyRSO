using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Services
{
    public class TimedHostedService : BackgroundService
    {
        private readonly ILogger logger;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider services)
        {
            this.logger = logger;
            this.services = services;
        }

        private IServiceProvider services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("TimedHostedService is starting.");

            stoppingToken.Register(() => logger.LogInformation("TimedHostedService is stopping."));

            TimeSpan delay = TimeSpan.FromMinutes(10);
            DateTime lastUpdateUtc = DateTime.UtcNow;

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = services.CreateScope())
                {
                    var newsService = scope.ServiceProvider.GetRequiredService<NewsService>();
                    logger.LogInformation("Looking for news.");
                    var lastestNews = await newsService.GetLatestNews(lastUpdateUtc);
                    lastUpdateUtc = DateTime.Now;
                    logger.LogInformation("Found {NewsCount} news.", lastestNews.Count);
                    var notificationsService = scope.ServiceProvider.GetRequiredService<NotificationsService>();
                    var pushNotificationSender = scope.ServiceProvider.GetRequiredService<IPushNotificationSender>();
                    foreach (var group in lastestNews)
                    {
                        if (group.Newses.Count == 0) continue;
                        var urls = await notificationsService.GetSubsciberUrlsAsync(group.CategorySlug, group.ProvinceSlug);
                        foreach(var news in group.Newses)
                        {
                            logger.LogInformation("Send push notifications.");
                            logger.LogInformation("News id: {Id}, category: {Category}, province: {Province}", news.NewsId, news.CategorySlug, news.ProvinceSlug);
                            logger.LogInformation("{UrlCount} urls", urls.Count);
                            await pushNotificationSender.SendPushNotifications(urls, news);
                        }
                    }
                    logger.LogInformation("Finished sending push notifications.");
                }

                await Task.Delay(delay, stoppingToken);
            }

            logger.LogInformation("TimedHostedService background task is stopping.");
        }
    }
}