namespace SocialPublisherWorker.Shared.Extensions;

public static class DateTimeExtensions
{
    public static DateOnly StartOfWeek(
        this DateOnly date,
        DayOfWeek startOfWeek = DayOfWeek.Monday)
    {
        var diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;

        return date.AddDays(-diff);
    }
}