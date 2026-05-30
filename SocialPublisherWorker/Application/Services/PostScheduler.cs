using Microsoft.EntityFrameworkCore;
using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Entities;
using SocialPublisherWorker.Domain.Enums;
using SocialPublisherWorker.Infrastructure.Persistence;
using SocialPublisherWorker.Shared.Extensions;

namespace SocialPublisherWorker.Application.Services;

public sealed class PostScheduler(
    IClock clock,
    AppDbContext dbContext,
    IPublicationRepository publicationRepo,
    ILogger<PostScheduler> logger)
    : IPostScheduler
{
    private readonly PostSchedulerOptions _options = new();
    
    public async Task<bool> ShouldPublishAsync(
        SocialPlatform platform,
        CancellationToken ct)
    {
        var now = clock.UtcNow;

        var localNow = now.ToLocalTime();

        if (localNow.DayOfWeek != _options.PublishDay)
        {
            logger.LogInformation("Today is not {@TargetDay}, skipping publish.", _options.PublishDay);
            return false;
        }

        if (TimeOnly.FromDateTime(localNow.DateTime)
            < _options.PublishAfter)
        {
            logger.LogInformation("Publish is available only after {@Time}. Skipping publish.", _options.PublishAfter);
            return false;
        }

        var weekStart = DateOnly
            .FromDateTime(localNow.Date)
            .StartOfWeek();

        var alreadyPublished = await publicationRepo.DoesPostPublicationExistAsync(platform, weekStart);
        return !alreadyPublished;
    }
}