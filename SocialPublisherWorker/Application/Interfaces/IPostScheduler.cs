using SocialPublisherWorker.Domain.Enums;

namespace SocialPublisherWorker.Application.Interfaces;

public interface IPostScheduler
{
    Task<bool> ShouldPublishAsync(SocialPlatform platform, CancellationToken ct);
}