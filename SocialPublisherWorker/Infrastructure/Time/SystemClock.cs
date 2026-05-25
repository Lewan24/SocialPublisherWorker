using SocialPublisherWorker.Application.Interfaces;

namespace SocialPublisherWorker.Infrastructure.Time;

public class SystemClock : IClock
{
    public DateTimeOffset UtcNow
        => DateTimeOffset.UtcNow;
}