namespace SocialPublisherWorker.Application.Interfaces;

public interface IPostScheduler
{
    Task<bool> ShouldPublishAsync(CancellationToken ct);
}