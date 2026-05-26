using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Entities;

namespace SocialPublisherWorker.Application.Services;

public class PostScheduler(IClock clock, SchedulerOptions options) : IPostScheduler
{
    public Task<bool> ShouldPublishAsync(CancellationToken ct)
    {
        //TODO: Implement logic with database
        return Task.FromResult(true);
    }
}