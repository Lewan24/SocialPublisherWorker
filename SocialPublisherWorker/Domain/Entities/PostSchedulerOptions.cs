namespace SocialPublisherWorker.Domain.Entities;

public sealed class PostSchedulerOptions
{
    public DayOfWeek PublishDay { get; init; } = DayOfWeek.Sunday;
    public TimeOnly PublishAfter { get; init; } = new(18, 0);
}