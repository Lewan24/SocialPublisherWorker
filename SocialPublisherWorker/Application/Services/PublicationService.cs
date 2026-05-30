using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Enums;
using SocialPublisherWorker.Infrastructure.Social.Facebook;

namespace SocialPublisherWorker.Application.Services;

public class PublicationService(
    ICalendarGenerator postGenerator,
    IPostScheduler postScheduler,
    ILogger<PublicationService> logger,
    FacebookPublisher facebookPublisher) : IPublicationService
{
    public async Task PublishWeeklyPostAsync(CancellationToken ct)
    {
        logger.LogInformation("Preparing new next week post...");
        var newPost = postGenerator.GenerateNextWeek();
        
        if(await postScheduler.ShouldPublishAsync(SocialPlatform.Facebook, ct))
        {
            logger.LogInformation("Publishing new post to facebook...");
            await facebookPublisher.PublishAsync(newPost, ct);
        }
    }
}