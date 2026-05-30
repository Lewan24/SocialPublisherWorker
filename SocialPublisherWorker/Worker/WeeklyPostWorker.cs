using SocialPublisherWorker.Application.Interfaces;

namespace SocialPublisherWorker.Worker;

public class WeeklyPostWorker(
    ILogger<WeeklyPostWorker> logger,
    IServiceScopeFactory scopeFactory,
    IClock clock)
    : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(30);
    
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation("Worker running at: {time} UTC", clock.UtcNow);
                
                using var scope = scopeFactory.CreateScope();
                  
                var publicationService = scope.ServiceProvider.GetRequiredService<IPublicationService>();
                await publicationService.PublishWeeklyPostAsync(ct);
                
                logger.LogInformation("Worker finished. Next run in {@Interval}", _interval);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Worker execution failed: {msg}", e.Message);
            }
            
            await Task.Delay(_interval, ct);
        }
    }
}