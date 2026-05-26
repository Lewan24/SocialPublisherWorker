using SocialPublisherWorker.Application.Interfaces;
using SocialPublisherWorker.Domain.Entities;

namespace SocialPublisherWorker.Application.Services;

public sealed class CalendarGenerator
    : ICalendarGenerator
{
    public CalendarPost GenerateNextWeek()
    {
        // TODO: Implement proper post generation
        return new CalendarPost
        {
            Content = "Test Content",
            Title = "Test Title",
            WeekStart = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(4))
        };
    }
}