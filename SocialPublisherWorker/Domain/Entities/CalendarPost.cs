namespace SocialPublisherWorker.Domain.Entities;

public sealed class CalendarPost
{
    public required string Caption { get; init; }

    public DateOnly WeekStart { get; init; }
}