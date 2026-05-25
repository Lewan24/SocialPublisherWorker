namespace SocialPublisherWorker.Domain.Entities;

public sealed class CalendarPost
{
    public required string Title { get; init; }

    public required string Content { get; init; }

    public DateOnly WeekStart { get; init; }
}