using SocialPublisherWorker.Application.Interfaces;

namespace SocialPublisherWorker.Worker;

public class WeeklyPostWorker(
    ILogger<WeeklyPostWorker> logger,
    IPublicationService publicationService,
    IPostScheduler scheduler) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                if (await scheduler.ShouldPublishAsync(ct))
                    await publicationService.PublishWeeklyPostAsync(ct);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Worker execution failed: {msg}", e.Message);
            }
            
            await Task.Delay(TimeSpan.FromSeconds(5), ct);
        }
    }
}