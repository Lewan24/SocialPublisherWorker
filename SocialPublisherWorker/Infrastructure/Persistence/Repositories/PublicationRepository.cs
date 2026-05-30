using Microsoft.EntityFrameworkCore;
using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Entities;
using SocialPublisherWorker.Domain.Enums;
using SocialPublisherWorker.Shared.Extensions;

namespace SocialPublisherWorker.Infrastructure.Persistence.Repositories;

public class PublicationRepository(AppDbContext dbContext, ILogger<PublicationRepository> logger) : IPublicationRepository
{
    public async Task<bool> DoesPostPublicationExistAsync(SocialPlatform platform, DateOnly weekStart)
    {
        logger.LogInformation("Checking if post publication exist with specified parameters: {@Platform} - {@WeekStart}...",  platform, weekStart);
        
        var result = await dbContext
            .PostPublications
            .AnyAsync(
                x => x.Platform == platform
                     && x.WeekStart == weekStart);
        
        logger.LogInformation("Specified publication {@Status} exist", result ? "DOES" : "DOES NOT");
        return result;
    }

    public async Task AddPostPublicationAsync(SocialPlatform platform, string externalPostId)
    {
        try
        {
            logger.LogInformation("Adding new post publication for {@Platform}...", platform);
            
            await dbContext.PostPublications.AddAsync(
                new PostPublication
                {
                    Id = Guid.NewGuid(),
                    Platform = platform,
                    WeekStart = DateOnly
                        .FromDateTime(DateTime.UtcNow)
                        .StartOfWeek(),
                    PublishedAtUtc = DateTimeOffset.UtcNow,
                    ExternalPostId = externalPostId
                });

            await dbContext.SaveChangesAsync();
            logger.LogInformation("Successfully added new post publication for {@Platform}", platform);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to add post publication");
            throw;
        }
    }
}