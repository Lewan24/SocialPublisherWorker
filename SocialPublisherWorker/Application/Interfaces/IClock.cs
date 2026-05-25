namespace SocialPublisherWorker.Application.Interfaces;

public interface IClock
{
    DateTimeOffset UtcNow { get; }
}