namespace SocialPublisherWorker.Domain.Entities;

public sealed class CalendarPost
{
    public required string PhotoUrl { get; init; }
    public required string Caption { get; init; }
}