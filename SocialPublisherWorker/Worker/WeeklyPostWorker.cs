using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Infrastructure.Social.Facebook;

namespace SocialPublisherWorker.Worker;

public class WeeklyPostWorker(
    ILogger<WeeklyPostWorker> logger,
    IServiceScopeFactory scopeFactory)
    : BackgroundService
{
    //TODO: Implement proper publishing only in sunday if the post was not published already
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                
                var scheduler = scope.ServiceProvider.GetRequiredService<IPostScheduler>();   
                var publicationService = scope.ServiceProvider.GetRequiredService<IPublicationService>();
                
                if (await scheduler.ShouldPublishAsync(ct))
                    await publicationService.PublishWeeklyPostAsync(ct);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Worker execution failed: {msg}", e.Message);
            }
            
            await Task.Delay(TimeSpan.FromMinutes(30), ct);
        }
    }
}