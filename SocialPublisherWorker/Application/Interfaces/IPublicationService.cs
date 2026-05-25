namespace SocialPublisherWorker.Application.Interfaces;

public interface IPublicationService
{
    Task PublishWeeklyPostAsync(CancellationToken ct);
}