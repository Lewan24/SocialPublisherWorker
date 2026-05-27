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
            Caption = "Schedule for xx.xx - xx.xx",
            WeekStart = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(4))
        };
    }
}